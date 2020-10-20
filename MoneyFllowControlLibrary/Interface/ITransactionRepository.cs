using System.Collections.ObjectModel;
using System.Linq;

namespace MoneyFllowControlLibrary.Model
{
    public interface ITransactionRepository
    {
        void Add(Transaction currentTransaction);
        void Delete(Transaction currentTransaction);
        public IQueryable<Transaction> GetAll();
        public IQueryable<Transaction> GetByTypeId(int typeId);
    }
}
