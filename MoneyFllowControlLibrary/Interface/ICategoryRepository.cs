using MoneyFllowControlLibrary.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace MoneyFllowControlLibrary.Interface
{
    public interface ICategoryRepository
    {
        IQueryable<Category> GetAll();
        IQueryable<Category> GetByTypeId(int typeId);
    }
}
