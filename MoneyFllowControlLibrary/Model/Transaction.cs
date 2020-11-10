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
    }
}
