using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyFllowControlLibrary.Context;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoneyFllowControlLibrary.Model.Tests
{
    //https://www.loganfranken.com/blog/517/mocking-dbset-queries-in-ef6/
    [TestClass()]
    public class TypeRepositoryTests
    {
        IApplicationContext db;
        List<Type> types;
        List<Category> categories;
        List<Transaction> transactions;

        [TestInitialize]
        public void Setup()
        {
            db = new MockContext();
            categories = new List<Category>
            {
               new Category {Id=1, Title = "Category1", TypeId=1 },
               new Category {Id=2, Title = "Category2", TypeId=1 },
               new Category {Id=3, Title = "Category3", TypeId=2 }
            };
            transactions = new List<Transaction>
            {
               new Transaction{Id=1, CategoryId=1, Date=new DateTime(), Description="desc1", Summ=100 },
               new Transaction{Id=2, CategoryId=2, Date=new DateTime(), Description="desc2", Summ=100 },
               new Transaction{Id=3, CategoryId=3, Date=new DateTime(), Description="desc3", Summ=100 },
            };
            types = new List<Type>
            {
               new Type() {Id=1, Title = "Type1" },
               new Type() {Id=2, Title = "Type2" },
               new Type() {Id=3, Title = "Type3" },
            };
            var mockTypes = new Mock<DbSet<Type>>();
            var mockCategories = new Mock<DbSet<Category>>();
            var mockTransactions = new Mock<DbSet<Transaction>>();

            mockTypes.As<IQueryable<Type>>().Setup(m => m.GetEnumerator()).Returns(types.AsQueryable().GetEnumerator());
            mockCategories.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(categories.AsQueryable().GetEnumerator());
            mockTransactions.As<IQueryable<Transaction>>().Setup(m => m.GetEnumerator()).Returns(transactions.AsQueryable().GetEnumerator());

            IApplicationContext mock = new Mock<IApplicationContext>().Object;
            db.Types = mockTypes.Object;
            db.Categories = mockCategories.Object;
            db.Transactions = mockTransactions.Object;
        }

        [TestMethod()]
        public void GetAll()
        {
            TypeRepository repository = new TypeRepository(db);
            CollectionAssert.AreEqual(repository.GetAll().ToList(), types);
        }
    }
}