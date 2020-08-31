using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyFllowControlLibrary.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int TypeId { get; set; }
        public Type Type { get; set; }
        public ICollection<Transaction> Transactions {get;set;}

        public Category()
        {
            Transactions = new List<Transaction>();
        }
    }
}
