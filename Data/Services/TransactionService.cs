using Microsoft.EntityFrameworkCore;
using FinanceTool.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Data.SqlClient;

namespace FinanceTool.Data.Services
{
    public class TransactionService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly UserDataService _userDataService;  
        public TransactionService(IDbContextFactory<ApplicationDbContext> dbContextFactory, AuthenticationStateProvider authenticationStateProvider, UserDataService userDataService) {
            _dbContextFactory = dbContextFactory;
            _authenticationStateProvider = authenticationStateProvider;
            _userDataService = userDataService;
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
        public async Task<decimal?> GetBalanceAsync() {
            using var context = _dbContextFactory.CreateDbContext();
            var userId = GetCurrentUserId();

            if (string.IsNullOrEmpty(userId))
                return 0;

            return await context.Transaction
                .Where(t => t.UserId == userId)
                .SumAsync(t => t.Amount);
        }

        public async Task AddTransactionAsync(Transaction transaction, string? userId = null) {
            using var context = _dbContextFactory.CreateDbContext();
            if (string.IsNullOrEmpty(userId)) {
                userId = GetCurrentUserId();
            }
            if (string.IsNullOrEmpty(userId)) {
               throw new UnauthorizedAccessException("User is not authenticated.");
            }
          
            transaction.UserId = userId;

            try {
                context.Add(transaction);
                await context.SaveChangesAsync();
                var newBalance = await context.Transaction
                    .Where(t => t.UserId == userId)
                    .SumAsync(t => t.TransactionType == TransactionType.Income ? t.Amount : -t.Amount);

                await _userDataService.UpdateBalanceAsync(userId, newBalance);
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
