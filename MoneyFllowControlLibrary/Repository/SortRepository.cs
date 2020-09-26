using MoneyFllowControlLibrary.Interface;
using System.Collections.Generic;

namespace MoneyFllowControlLibrary.Model
{
    public class SortRepository : ISortTransaction
    {
        public List<string> GetAll()
        {
            var Transactions = new List<string>() { "123", "456" };
            return Transactions;
        }
    }
}
