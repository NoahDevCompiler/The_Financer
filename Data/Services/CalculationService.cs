using FinanceTool.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceTool.Data.Services
{
    public class CalculationService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly TransactionService _transactionService;

        public CalculationService(IDbContextFactory<ApplicationDbContext> dbContextFactory, TransactionService transactionService)
        {
            _dbContextFactory = dbContextFactory;
            _transactionService = transactionService;
        }
    }
}
