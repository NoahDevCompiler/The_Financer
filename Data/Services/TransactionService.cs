using Microsoft.EntityFrameworkCore;
using FinanceTool.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace FinanceTool.Data.Services
{
    public class TransactionService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public TransactionService(IDbContextFactory<ApplicationDbContext> dbContextFactory, AuthenticationStateProvider authenticationStateProvider) {
            _dbContextFactory = dbContextFactory;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public string? GetCurrentUserId() {
            var authState = _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.Result.User; 
            
            if (user.Identity.IsAuthenticated) {
                return user.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            }
            throw new UnauthorizedAccessException("User is not authenticated.");

        }   

        public async Task<List<Transaction>> GetTransactionsAsync() {
            using var context = _dbContextFactory.CreateDbContext();
            var userId = GetCurrentUserId();

            if(string.IsNullOrEmpty(userId)) {
                return new List<Transaction>();
            }
            try {
                return await context.Transaction.Where(t => t.UserId == userId).ToListAsync();
            }
            catch (Exception ex) {
                //
                return null;
            }
        }
        public async Task AddTransactionAsync(Transaction transaction) {
            using var context = _dbContextFactory.CreateDbContext();
            var userId = GetCurrentUserId();    

            if (string.IsNullOrEmpty(userId)) {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            transaction.UserId = userId;

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
