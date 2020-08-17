using MoneyFllowControlLibrary.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyFllowControlLibrary.Model
{
    public class SortRepository : ISortTransaction
    {
        public List<string> GetAll()
        {
            var Transactions = new List<string>() { "123","456"};
            return Transactions;
        }
    }
}
