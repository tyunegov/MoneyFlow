using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyFllow.Model;
using MoneyFllowControlLibrary;
using MoneyFllowControlLibrary.Context;
using System;
using Type = MoneyFllowControlLibrary.Model.Type;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyFllow.Model.Tests
{
    [TestClass()]
    public class TransactionRepositoryTests
    {
        TransactionRepository repository;

        public TransactionRepositoryTests()
        {
            var db = new MockContext();
          //  db.Create();
            repository = new TransactionRepository(db);
        }

        [TestMethod()]
        public void GetAll()
        {
            var transactions = repository.GetAll();

  //          Assert.IsTrue(transactions.All(u => u.CategoryId == u.Category.Id));
            Assert.AreEqual(3, repository.GetAll().Count());
        }


        [TestMethod()]
        public void GetByTypeId()
        {
            int typeId = 1;
            var transactions = repository.GetByTypeId(typeId);
            foreach (var v in transactions)
            {
                Console.WriteLine(v.Category.Name);
            }
            Assert.IsTrue(transactions.All(u => u.Category.TypeId == typeId));
            Assert.AreEqual(2, transactions.Count());
        }

        [TestMethod()]
        public void FilterTest()
        {
            Assert.Fail();
        }
    }
}