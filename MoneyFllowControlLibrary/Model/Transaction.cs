using System;

namespace MoneyFllowControlLibrary.Model
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string Description { get; set; }
        public decimal Summ { get; set; }

        public Transaction()
        {
            Category = new Category();
        }

        public override bool Equals(object obj)
        {
            Transaction transaction = obj as Transaction;
            Console.WriteLine(Id + " " + transaction.Id);
            Console.WriteLine((Category == null) + " " + (transaction.Category == null));
            return transaction != null &&
                   Id == transaction.Id &&
                   (((Date == null && (transaction.Date == null)) ? true : Date.Equals(transaction.Date))) &&
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
