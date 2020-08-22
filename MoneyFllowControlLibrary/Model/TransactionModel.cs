using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyFllowControlLibrary.Model
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
        public string Description { get; set; }
        public decimal Summ { get; set; }

        public TransactionModel() { }

        public TransactionModel(DateTime date, CategoryModel category, string description, int summ)
        {
            Date = date;
            Category = category;
            Description = description;
            Summ = summ;
        }
    }
}
