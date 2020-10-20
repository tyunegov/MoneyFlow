using System.Linq;

namespace MoneyFllowControlLibrary.Interface
{
    public interface ITypeTransactionRepository
    {
        IQueryable<Model.Type> GetAll();
    }
}
