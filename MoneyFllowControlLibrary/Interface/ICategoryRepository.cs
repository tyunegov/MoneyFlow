using MoneyFllowControlLibrary.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MoneyFllowControlLibrary.Interface
{
    public interface ICategoryRepository
    {
        ObservableCollection<Category> GetAll();
        ObservableCollection<Category> GetByType(Model.Type selectedType);
    }
}
