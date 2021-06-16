using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ML;
using Microsoft.ML.Data;
using WebApp;
using static WebApp.MLModel;

namespace ScoringWebApp.Pages
{
    public class ScoreModel
    {
        public ScoreModel(string party, float score)
        {
            Party = party ?? throw new ArgumentNullException(nameof(party));
            Score = score;
        }

        public string Party { get; }

        public float Score { get; }
    }

    public class IndexModel : PageModel
    {
        private readonly PredictionEnginePool<ModelInput, ModelOutput> _predictionEnginePool;
        private readonly ILogger<IndexModel> _logger;

        [Required(ErrorMessage = "Bitte geben Sie eine Rede an.")]
        [BindProperty]
        public string Speach { get; set; }

        public string Prediction { get; set; }

        public List<ScoreModel> Scores { get; set; }

        public IndexModel(PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool, ILogger<IndexModel> logger)
        {
            _predictionEnginePool = predictionEnginePool;
            _logger = logger;
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            ModelInput inputData = new ModelInput()
            {
                Content = this.Speach
            };

            var predictionResult = _predictionEnginePool.Predict(modelName: Constants.ModelName, inputData);

            this.Scores = BuildScores(predictionResult);
            this.Prediction = predictionResult.Prediction;
        }

        private List<ScoreModel> BuildScores(ModelOutput predictionResult)
        {
            var parties = GetPartyNames();
            var scores = new List<ScoreModel>();
            for (int i = 0; i < parties.Count; i++)
            {
                scores.Add(new ScoreModel(parties[i], (float)Math.Round(predictionResult.Score[i], 3)));
            }

            return scores.OrderByDescending(s => s.Score).ToList();
        }

        private List<string> GetPartyNames()
        {
            var partyNames = new List<string>();

            var column = _predictionEnginePool.GetPredictionEngine(Constants.ModelName).OutputSchema.GetColumnOrNull("Party");
            if (column.HasValue)
            {
                VBuffer<ReadOnlyMemory<char>> vbuffer = new VBuffer<ReadOnlyMemory<char>>();
                column.Value.GetKeyValues(ref vbuffer);

                foreach (ReadOnlyMemory<char> denseValue in vbuffer.DenseValues())
                    partyNames.Add(denseValue.ToString());
            }

            return partyNames;
        }
    }
}
