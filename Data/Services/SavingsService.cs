using FinanceTool.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinanceTool.Data.Services
{
    public class SavingsService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        public SavingsService(
            IDbContextFactory<ApplicationDbContext> contextFactory
        ) {
            _contextFactory = contextFactory;
        }
        public async Task AddSavingAsync(Savings saving) {
            
            using var context = _contextFactory.CreateDbContext();           
            try {
                context.Add(saving);
                await context.SaveChangesAsync();
            }
            catch (Exception ex) {
                Console.WriteLine(saving);
                Console.WriteLine("Fehler beim Speichern:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
            
        }
        public async Task<List<Savings>> GetSavingsAsync() {
            using var context = _contextFactory.CreateDbContext();
            return await context.Savings.ToListAsync();
        }
        
    }
}
    
