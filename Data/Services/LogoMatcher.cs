using FinanceTool.Models;
using FuzzySharp;

namespace FinanceTool.Data.Services
{
    public class LogoMatcher
    {
       private readonly Dictionary<string, string> logos = new Dictionary<string, string> {
       { "spotify", "assets/transactions/spotify.svg" },
       { "netflix", "assets/transactions/netflix.svg" },
       { "amazon", "assets/transactions/amazon.svg" },
       { "paypal", "assets/transactions/paypal.svg" },
       };
        
        public string GetBestMatchingLogo(string tName, TransactionType? tType) {
            var best = logos
                .Select(entry => new {
                    Icon = entry.Value,
                    Score = Fuzz.Ratio(entry.Key, tName.ToLower())
                })
                .OrderByDescending(x => x.Score)
                .FirstOrDefault();

            if (best.Score > 50) {
                return best.Icon;

            } else if (tType == TransactionType.Income) {
                return "assets/transactions/income.svg";

            } else return "assets/transactions/expense.svg";
        }
    }   
}
