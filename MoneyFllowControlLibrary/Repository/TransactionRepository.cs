using MoneyFllowControlLibrary.Context;
using MoneyFllowControlLibrary.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

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

        public Transaction GetById(int id)
        {
            return db.Transactions.Where(x => x.Id == id).FirstOrDefault();
        }


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

        public IQueryable<Transaction> Filter(MoneyFllowControlLibrary.Model.Type selectedFilterType)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Transaction> Sort(string selectedSort)
        {
            throw new NotImplementedException();
        }
    }
}
