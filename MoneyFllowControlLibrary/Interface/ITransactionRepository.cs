using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MoneyFllowControlLibrary.Model
{
    public interface ITransactionRepository
    {
        void Add(TransactionModel currentTransaction);
        void Delete(TransactionModel currentTransaction);
        public ObservableCollection<TransactionModel> GetAll();
        ObservableCollection<TransactionModel> Filter(TypeModel selectedFilterType);
        ObservableCollection<TransactionModel> Sort(string selectedSort);
    }
}
