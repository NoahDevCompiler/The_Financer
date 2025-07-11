using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceTool.Models
{
    public enum TransactionType
    {
        Income,
        Expense,
        Transfer
    }
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } 

        [Required]
        public string TransactionName { get; set; }

        [Required]
        public decimal? Amount { get; set; }

        public DateTime TransactionDate { get; set; }
        public TransactionType? TransactionType { get; set; }
        public string Category { get; set; }

        public User User { get; set; }
    }
}
