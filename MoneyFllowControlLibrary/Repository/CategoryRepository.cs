using MoneyFllowControlLibrary.Interface;
using MoneyFllowControlLibrary.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MoneyFllowControlLibrary.Controller
{
    public class CategoryRepository : ICategoryRepository
    {
        public ObservableCollection<Category> GetAll()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Category> GetByType(Model.Type selectedType)
        {
            throw new NotImplementedException();
        }
    }
}
