using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MoneyFllow.Model
{
    public interface ITransactionRepository
    {
        void Add(Transaction currentTransaction);
        void Delete(Transaction currentTransaction);
        public ObservableCollection<Transaction> AllTransaction();
        ObservableCollection<Transaction> Filter(int selectedFilter);
        ObservableCollection<Transaction> Sort(int selectedSort);
    }
}
