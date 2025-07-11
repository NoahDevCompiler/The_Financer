using Microsoft.EntityFrameworkCore;
using FinanceTool.Models;

namespace FinanceTool.Data.Services
{
    public class TransactionService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public TransactionService(IDbContextFactory<ApplicationDbContext> dbContextFactory) {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<Transaction>> GetTransactionsAsync() {
            using var context = _dbContextFactory.CreateDbContext();

            try {
                return await context.Transaction.ToListAsync();
            }
            catch (Exception ex) {
                //
                return null;
            }
        }
        public async Task AddTransactionAsync(Transaction transaction) {
            using var context = _dbContextFactory.CreateDbContext();

            try {
                context.Add(transaction);
                await context.SaveChangesAsync();
            }
            catch (Exception ex) {
                Console.WriteLine(transaction);
                Console.WriteLine("Fehler beim Speichern:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }

        }

    }
}
