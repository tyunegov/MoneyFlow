using Microsoft.EntityFrameworkCore;
using MoneyFllowControlLibrary.Context;
using MoneyFllowControlLibrary.Model;
using Type = MoneyFllowControlLibrary.Model.Type;

namespace MoneyFllowControlLibrary
{
    public class MockContext : IApplicationContext
    {
        public MockContext()
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Type> Types { get; set; }

        public int SaveChanges()
        {
            return 0;
        }
    }
}