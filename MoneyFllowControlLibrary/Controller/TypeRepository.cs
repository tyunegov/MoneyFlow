using MoneyFllowControlLibrary.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MoneyFllowControlLibrary.Model
{
    public class TypeRepository : ITypeTransactionRepository
    {
        public ObservableCollection<TypeModel> GetAll()
        {
            var Transactions = new ObservableCollection<TypeModel>() { 
                new TypeModel() {Id=1, Title="Доход" },
                new TypeModel() {Id=2, Title="Расход" },
                new TypeModel() {Id=3, Title="Перевод" },
            };
            return Transactions;
        }
    }
}
