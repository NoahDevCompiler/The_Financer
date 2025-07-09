using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceTool.Models
{
    public class Savings
    {
        public string Id { get; set; }
        public string UserId { get; set; } 
        public float? TargetAmount { get; set; }    
        public float? CurrentAmount { get; set; }
        public DateTime TargetDate { get; set; }
        public float? ForeCast {  get; set; }
                                                                    
        public User User { get; set; }
    }       
}       
    