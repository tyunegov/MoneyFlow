using Microsoft.EntityFrameworkCore;
using MoneyFllowControlLibrary.Model;
using MoneyFllowControlLibrary.Repository;

namespace MoneyFllowControlLibrary.Context
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Type> Types { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public ApplicationContext() : base()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Filename=MoneyFllow.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Transaction>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.CategoryId);
            modelBuilder.Entity<Category>()
                .HasOne(x => x.Type)
                .WithMany(x => x.Categories)
                .HasForeignKey(x => x.TypeId);
        }
    }
}
