using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        [Required(ErrorMessage = "Bitte geben Sie eine Rede an.")]
        [BindProperty]
        public string Speach { get; set; }

        public string Prediction { get; set; }

        public List<ScoreModel> Scores { get; set; }

        public IndexModel()
        {
           
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }
           
        }
    }
}
