using MoneyFllowControlLibrary.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MoneyFllowControlLibrary.Model
{
    public class TypeRepository : ITypeTransactionRepository
    {
        public ObservableCollection<Type> GetAll()
        {
            var Transactions = new ObservableCollection<Type>() { 
                new Type() {Id=1, Title="Доход" },
                new Type() {Id=2, Title="Расход" },
                new Type() {Id=3, Title="Перевод" },
            };
            return Transactions;
        }
    }
}
