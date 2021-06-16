using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DataProvider
{
    public class SpeachItem
    {
        public string Link { get; set; }
        public string Content { get; set; }
        public string Party { get; set; }
    }

    public class SpeachProvider
    {
        private const string BaseUrl = "https://www.parlament.gv.at/PAKT/VHG/XXVII/NRSITZ/NRSITZ_{0}/index.shtml#tab-Sten.Protokoll";

        public SpeachProvider()
        {
        }

        public Task<List<SpeachItem>> GetSpeachesAsync(int number)
        {
            var url = string.Format(BaseUrl, number.ToString("D5"));

            using var driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);

            var speachLinks = driver.FindElementsByClassName("redeEntry")
                .Select(l => l.GetAttribute("href"))
                .ToArray();

            var list = new List<SpeachItem>();

            foreach (var l in speachLinks)
            {
                try
                {
                    var item = new SpeachItem() { Link = l };
                    driver.Navigate().GoToUrl(item.Link);

                    var contentElement = driver.FindElementsByClassName("MsoNormal");
                    if (TryParseContentElement(contentElement, out var content, out var party))
                    {
                        item.Content = content;
                        item.Party = party;
                        list.Add(item);
                        Console.WriteLine($"Successfully added speach from {party}");
                    }
                }
                catch(Exception ex)
                {
                    System.Console.WriteLine($"Failed to parse speach from {l}.");
                }                
            }

            return Task.FromResult(list);
        }

        private bool TryParseContentElement(IEnumerable<IWebElement> elements, out string content, out string party)
        {
            content = null;
            party = GetParty(elements.First().Text);
            if(string.IsNullOrEmpty(party))
            {
                return false;
            }

            content = GetContent(elements);
            if(string.IsNullOrEmpty(content))
            {
                return false;
            }

            return true;
        }

        private string GetContent(IEnumerable<IWebElement> elements)
        {
            if(!elements.Any())
            {
                return null;
            }

            StringBuilder sb = new StringBuilder();

            // Remove speaker and add first content block
            var firstElementText = elements.First().Text;
            var endIndex = firstElementText.IndexOf("):");
            if(endIndex < 0)
            {
                sb.Append(firstElementText);
            }
            else
            {
                var firstElementWithoutSpeaker = firstElementText.Substring(endIndex + 2);
                sb.Append(firstElementWithoutSpeaker);
            }

            foreach (var e in elements.Skip(1))
            {
                sb.Append(e.Text);
            }
            return sb.ToString();
        }

        private string GetParty(string text)
        {
            var endIndex = text.IndexOf("):");
            if (endIndex < 0)
            {
                return null;
            }

            var startIndex = text.IndexOf("(");
            return text.Substring(startIndex + 1, endIndex - startIndex + 1 - 2 );
        }
    }
}
