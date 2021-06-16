using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataProvider
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Loading speaches ...");
            using var context = new SpeachDbContext();
            var speachCount = context.Speaches.Count();
            Console.WriteLine($"Currently {speachCount} speaches in database.");
            var feedProvider = new SpeachProvider();
            for(int i=1; i<107; i++)
            {
                var speaches = await feedProvider.GetSpeachesAsync(i);
                var entities = speaches.Select(s => new Speach() { Content = s.Content, Party = s.Party }).ToList();
                context.Speaches.AddRange(entities);
                await context.SaveChangesAsync();
                Console.WriteLine($"Saved {speaches.Count} speaches from meeting {i}.");
            }            
        }
    }
}
