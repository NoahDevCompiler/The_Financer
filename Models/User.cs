using Microsoft.AspNetCore.Identity;
namespace FinanceTool.Models
{
    public class User : IdentityUser
    {
        public decimal? Balance { get; set; }
        public DateTime? Birthdate { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Savings> Savings { get; set; }

    }
}
