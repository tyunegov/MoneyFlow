using Microsoft.EntityFrameworkCore;
using MoneyFllowControlLibrary.Model;

namespace MoneyFllowControlLibrary.Context
{
    public interface IApplicationContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        public DbSet<Type> Types { get; set; }

        int SaveChanges();
    }
}