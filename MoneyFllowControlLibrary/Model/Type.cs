using System;
using System.Collections.Generic;
using System.Linq;

namespace MoneyFllowControlLibrary.Model
{
    public class Type
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Category> Categories { get; set; }

        public Type()
        {
            Categories = new List<Category>();
        }
    }
}
