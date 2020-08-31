using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MoneyFllowControlLibrary.Model
{
    public interface ITransactionRepository
    {
        void Add(Transaction currentTransaction);
        void Delete(Transaction currentTransaction);
        public ObservableCollection<Transaction> GetAll();
        ObservableCollection<Transaction> Filter(Type selectedFilterType);
        ObservableCollection<Transaction> Sort(string selectedSort);
    }
}
