using MoneyFllowControlLibrary.Model;
using System;
using System.Collections.ObjectModel;

namespace MoneyFllow.Model
{
    public class TransactionRepository:ITransactionRepository
    {
        ObservableCollection<TransactionModel> _transaction;
        public ObservableCollection<TransactionModel> GetAll()
        {
                if (_transaction == null)
                    _transaction = GenerateTransactionRepository();
                return _transaction;            
        }

        private static ObservableCollection<TransactionModel> GenerateTransactionRepository()
        {
            var Transactions = new ObservableCollection<TransactionModel>();
            Transactions.Add(new TransactionModel(new DateTime(2020, 01, 30), new TypeModel() {Title="123" },new CategoryModel() {Title="Продукты" },"Description", 400));
            return Transactions;
        }

        public void Add(TransactionModel currentTransaction)
        {
             throw new NotImplementedException();
        }

        public void Delete(TransactionModel currentTransaction)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<TransactionModel> Filter(TypeModel selectedFilterType)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<TransactionModel> Sort(string selectedSort)
        {
            throw new NotImplementedException();
        }
    }
}
