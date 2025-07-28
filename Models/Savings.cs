using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceTool.Models
{
    public class Savings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } 

        [Required]
        public string SavingGoal { get; set; }

        [Required]
        public decimal? TargetAmount { get; set; }    
        public DateTime TargetDate { get; set; }
        public decimal? ForeCast {  get; set; }
                                                                    
        public User User { get; set; }
    }       
}       
    