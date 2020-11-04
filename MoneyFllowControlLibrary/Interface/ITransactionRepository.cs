using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MoneyFllowControlLibrary.Model
{
    public interface ITransactionRepository
    {
        int Add(Transaction currentTransaction);
        void Delete(Transaction currentTransaction);
        IQueryable<Transaction> GetAll();
        IQueryable<Transaction> GetByTypeId(int typeId);
        /// <summary>
        /// Запрос по типу и времени начала и окончания
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public IQueryable<Transaction> Filter(int typeId, DateTime dateStart, DateTime dateEnd);
    }
}
