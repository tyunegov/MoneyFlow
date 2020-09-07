using System;
using System.Collections.Generic;
using System.Linq;

namespace MoneyFllowControlLibrary.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int TypeId { get; set; }
        public Type? Type { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

        public override bool Equals(object obj)
        {
            Category category = obj as Category;
            return category != null &&
                   Id == category.Id &&
                   Title == category.Title &&
                   TypeId == category.TypeId &&
                    ((Transactions == null && (category.Transactions == null)) ? true :
                    !Transactions.Except(category.Transactions).Any() &&
                    Transactions.Count() == category.Transactions.Count()) &&
                    ((Type == null && (category.Type == null)) ? true : Type.Equals(category.Type));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Title, TypeId, Type, Transactions);
        }
    }
}
