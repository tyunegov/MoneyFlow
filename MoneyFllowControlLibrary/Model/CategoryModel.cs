using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyFllowControlLibrary.Model
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int TypeId { get; set; }
        public Type Type { get; set; }
        public ICollection<TransactionModel> Transactions {get;set;}

        public CategoryModel()
        {
            Transactions = new List<TransactionModel>();
        }
    }
}
