using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyFllowControlLibrary.Model
{
    public class Transaction
    {
        [ForeignKey("Id")]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public decimal Summ { get; set; }


        public override bool Equals(object obj)
        {
            Transaction transaction = obj as Transaction;
            if (transaction == null) return false;
            if (GetHashCode() == transaction.GetHashCode()) return true;
            return transaction != null &&
                   Id == transaction.Id &&
                   ((Date == null && (transaction.Date == null)) || Date.Equals(transaction.Date)) &&
                   CategoryId == transaction.CategoryId &&
                   (((Category == null && (transaction.Category == null)) ? true : Category.Equals(transaction.Category))) &&
                    Description == transaction.Description &&
                    Summ == transaction.Summ;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Date, CategoryId, Category, Description, Summ);
        }
    }
}
