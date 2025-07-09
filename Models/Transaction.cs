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
        public string Id { get; set; }
        public string UserId { get; set; } 
        public float? Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType TransactionType { get; set; }

        public User User { get; set; }
    }
}
