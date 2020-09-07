using MoneyFllowControlLibrary.Context;
using MoneyFllowControlLibrary.Interface;
using System.Linq;

namespace MoneyFllowControlLibrary.Model
{
    public class TypeRepository : ITypeTransactionRepository
    {
        IApplicationContext db;
        public TypeRepository()
        {
            db = new ApplicationContext();
        }

        public TypeRepository(IApplicationContext db)
        {
            this.db = db;
        }

        public IQueryable<Type> GetAll()
        {
            return db.Types;
        }
    }
}
