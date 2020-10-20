using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using MoneyFllowControlLibrary.Context;
using MoneyFllowControlLibrary.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Type = MoneyFllowControlLibrary.Model.Type;

namespace MoneyFllow.Model
{
    public class TransactionRepository : ITransactionRepository
    {
        IApplicationContext db;
        public TransactionRepository()
        {
            db = new ApplicationContext();
        }
        public TransactionRepository(IApplicationContext db)
        {
            this.db = db;
        }
        /// <summary>
        /// Выборка всех транзакций
        /// </summary>
        /// <returns></returns>
        public IQueryable<Transaction> GetAll()
        {
            var transactions = db.Transactions;
            if (transactions.Count() != 0)
            {
                foreach (var v in transactions)
                {
                    v.Category = (from p in db.Categories
                                  where p.Id == v.CategoryId
                                  select p).FirstOrDefault();
                    v.Category.Type = (from p in db.Types
                                       where p.Id == v.Category.TypeId
                                       select p).FirstOrDefault();
                }
            }
            return transactions;
        }
        /// <summary>
        /// Получение транзакций по Id
        /// </summary>
        /// <param name="id">Идентификатор транзакции</param>
        /// <returns></returns>
        public Transaction GetById(int id)
        {
            return db.Transactions.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Добавление транзакции
        /// </summary>
        public void Add(Transaction transaction)
        {
            db.Transactions.Add(new Transaction()
            {
                CategoryId = transaction.Category.Id,
                Description = transaction.Description,
                Date=transaction.Date,
                Summ=transaction.Summ,
            });
            db.SaveChanges();
        }

        public void Delete(Transaction currentTransaction)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Выборка всех транзакций по TypeId
        /// </summary>
        /// <param name="typeId">Идентификатор для Type в БД</param>
        /// <returns></returns>
        public IQueryable<Transaction> GetByTypeId(int typeId)
        {
            var transactions =from tr in db.Transactions
                              join c in db.Categories on tr.CategoryId equals c.Id
                              where(c.TypeId==typeId)
                              select new Transaction()
                              {
                                  Id=tr.Id,
                                  Category=c,
                                  Date=tr.Date,
                                  Description=tr.Description,
                                  CategoryId=tr.CategoryId,
                                  Summ=tr.Summ
                              };
            return transactions;
        }
    }
}
