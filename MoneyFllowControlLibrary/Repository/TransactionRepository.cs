using MoneyFllowControlLibrary.Model;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using MoneyFllowControlLibrary.Context;

namespace MoneyFllow.Model
{
    public class TransactionRepository:ITransactionRepository
    {
        ApplicationContext db;
        public TransactionRepository()
        {
            db = new ApplicationContext();
        }

        public ObservableCollection<Transaction> GetAll()
        {
            var transactions = new ObservableCollection<Transaction>(db.Transactions.ToList());
            if (transactions.Count==0)
                    transactions = GenerateTransactionRepository();
                return transactions;            
        }

        public Transaction GetById(int id)
        {
            return db.Transactions.Where(x => x.Id == id).FirstOrDefault();
        }

        private static ObservableCollection<Transaction> GenerateTransactionRepository()
        {
            var Transactions = new ObservableCollection<Transaction>();
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), new Category() { Title = "Продукты" }, "Description", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), new Category() { Title = "ЗП" }, "", 40000));
            return Transactions;
        }

        public void Add(Transaction currentTransaction)
        {
             throw new NotImplementedException();
        }

        public void Delete(Transaction currentTransaction)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Transaction> Filter(MoneyFllowControlLibrary.Model.Type selectedFilterType)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Transaction> Sort(string selectedSort)
        {
            throw new NotImplementedException();
        }
    }
}
