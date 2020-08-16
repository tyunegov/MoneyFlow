using MoneyFllowControlLibrary.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MoneyFllowControlLibrary.Model
{
    public class TypeTransaction : ITypeTransactionRepository
    {
        public ObservableCollection<string> GetAll()
        {
            var Transactions = new ObservableCollection<string>() { "123", "456" };
            return Transactions;
        }
    }
}
