using Microsoft.AspNetCore.Identity;
namespace FinanceTool.Models
{
    public class User : IdentityUser
    {
        public float? Capital { get; set; }
        public DateTime? Birthdate { get; set; }

    }
}
