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
        public ObservableCollection<CategoryModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<CategoryModel> GetByType(TypeModel selectedType)
        {
            throw new NotImplementedException();
        }
    }
}
