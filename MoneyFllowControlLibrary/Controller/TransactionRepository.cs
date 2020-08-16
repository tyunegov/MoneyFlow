using MoneyFllowControlLibrary.Model;
using System;
using System.Collections.ObjectModel;

namespace MoneyFllow.Model
{
    public class TransactionRepository:ITransactionRepository
    {
        ObservableCollection<Transaction> _transaction;
        public ObservableCollection<Transaction> GetAll()
        {
                if (_transaction == null)
                    _transaction = GenerateTransactionRepository();
                return _transaction;            
        }

        private static ObservableCollection<Transaction> GenerateTransactionRepository()
        {
            var Transactions = new ObservableCollection<Transaction>();
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Продукты", "", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Что-то жизненно важное", "А хз, не придумал", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Продукты", "", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Что-то жизненно важное", "А хз, не придумал", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Продукты", "", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Что-то жизненно важное", "А хз, не придумал", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Продукты", "", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Что-то жизненно важное", "А хз, не придумал", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Продукты", "", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Что-то жизненно важное", "А хз, не придумал", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Продукты", "", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Что-то жизненно важное", "А хз, не придумал", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Продукты", "", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Что-то жизненно важное", "А хз, не придумал", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Продукты", "", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Что-то жизненно важное", "А хз, не придумал", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Продукты", "", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Что-то жизненно важное", "А хз, не придумал", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Продукты", "", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Что-то жизненно важное", "А хз, не придумал", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Продукты", "", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Что-то жизненно важное", "А хз, не придумал", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Продукты", "", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Что-то жизненно важное", "А хз, не придумал", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Продукты", "", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Что-то жизненно важное", "А хз, не придумал", 400));
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

        public ObservableCollection<Transaction> Filter(TypeTransaction selectedFilterType)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Transaction> Sort(SortTransaction selectedSort)
        {
            throw new NotImplementedException();
        }
    }
}
