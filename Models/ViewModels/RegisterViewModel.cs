using System.ComponentModel.DataAnnotations;
namespace FinanceTool.Models.ViewModels
{
    public class RegisterViewModel
    {
        public string Email { get; set; } 

        public string Password { get; set; }

        public string Username { get; set; }
        public decimal? Balance { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
