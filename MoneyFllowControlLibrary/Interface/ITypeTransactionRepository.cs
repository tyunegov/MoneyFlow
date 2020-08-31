using MoneyFllowControlLibrary.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MoneyFllowControlLibrary.Interface
{
    public interface ITypeTransactionRepository
    {
        ObservableCollection<Model.Type> GetAll();
    }
}
