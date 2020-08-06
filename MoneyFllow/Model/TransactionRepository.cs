using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MoneyFllow.Model
{
    static class TransactionRepository
    {
        static ObservableCollection<Transaction> _transaction;
        public static ObservableCollection<Transaction> AllTransaction
        {
            get
            {
                if (_transaction == null)
                    _transaction = GenerateTransactionRepository();
                return _transaction;
            }
        }

        private static ObservableCollection<Transaction> GenerateTransactionRepository()
        {
            var Transactions = new ObservableCollection<Transaction>();
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Продукты", "", 400));
            Transactions.Add(new Transaction(new DateTime(2020, 01, 30), "Что-то жизненно важное", "А хз, не придумал", 400));
            return Transactions;
        }
    }
}
