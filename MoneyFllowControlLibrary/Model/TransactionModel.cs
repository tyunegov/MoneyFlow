using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyFllowControlLibrary.Model
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TypeModel Type { get; set; }
        public CategoryModel Category { get; set; }
        public string Description { get; set; }
        public decimal Summ { get; set; }

        public TransactionModel() { }

        public TransactionModel(DateTime date, TypeModel type, CategoryModel category, string description, int summ)
        {
            Date = date;
            Type = type;
            Category = category;
            Description = description;
            Summ = summ;
        }
    }
}
