using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyFllowControlLibrary.Model
{
    public class TypeModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<CategoryModel> Categories { get; set; }

        public TypeModel()
        {
            Categories = new List<CategoryModel>();
        }
    }
}
