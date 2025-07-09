using FinanceTool.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinanceTool.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Savings> Savings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<Savings>()
                .HasOne(t => t.User)
                .WithMany(u => u.Savings)
                .HasForeignKey(t => t.UserId);
            
            builder.Entity<Transaction>()
                .HasOne(s => s.User)
                .WithMany(t => t.Transactions)
                .HasForeignKey(s => s.UserId);
        }
    }
}
