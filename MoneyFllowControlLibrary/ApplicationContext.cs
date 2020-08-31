using MoneyFllowControlLibrary.Model;
using System.Data.Entity;

namespace MoneyFllowControlLibrary.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext():base("MoneyFllow")
        {

        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MoneyFllowControlLibrary.Model.Type> Types { get; set; }
    }
}
