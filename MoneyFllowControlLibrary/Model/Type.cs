using System;
using System.Collections.Generic;
using System.Linq;

namespace MoneyFllowControlLibrary.Model
{
    public class Type
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<Category> Categories { get; set; }

        public Type()
        {
            Categories = new List<Category>();
        }

        public override bool Equals(object obj)
        {
            Type type = obj as Type;
            if (type == null) return false;
            if (GetHashCode() == type.GetHashCode()) return true;
            return type != null &&
               ((Categories == null && (type.Categories == null)) || (!Categories.Except(type.Categories).Any() &&
               Categories.Count() == type.Categories.Count())) &&
                Id == type.Id &&
                Title == type.Title;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Title, Categories);
        }
    }
}
