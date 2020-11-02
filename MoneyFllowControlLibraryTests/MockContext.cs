using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using MoneyFllowControlLibrary.Context;
using MoneyFllowControlLibrary.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Type = MoneyFllowControlLibrary.Model.Type;

namespace MoneyFllowControlLibrary
{
    public class MockContext : IApplicationContext
    {
        public MockContext Create()
        {
            var categories = new List<Category>
            {
               new Category {Id=1, Name = "Category1", TypeId=1 },
               new Category {Id=2, Name = "Category2", TypeId=1 },
               new Category {Id=3, Name = "Category3", TypeId=2 }
            };
            var transactions = new List<Transaction>
            {
               new Transaction{Id=1, CategoryId=1, Date=new DateTime(), Description="desc1", Summ=100 },
               new Transaction{Id=2, CategoryId=2, Date=new DateTime(), Description="desc2", Summ=100 },
               new Transaction{Id=3, CategoryId=3, Date=new DateTime(), Description="desc3", Summ=100 },
            };
            var types = new List<Type>
            {
               new Type() {Id=1, Name = "Type1" },
               new Type() {Id=2, Name = "Type2" },
               new Type() {Id=3, Name = "Type3" },
            };
            var mockTypes = new Mock<DbSet<Type>>();
            var mockCategories = new Mock<DbSet<Category>>();
            var mockTransactions = new Mock<DbSet<Transaction>>();

            mockTransactions.As<IQueryable<IQueryable<Transaction>>>().Setup(m => m.Provider).Returns(transactions.AsQueryable().Provider);
            mockTransactions.As<IQueryable<IQueryable<Transaction>>>().Setup(m => m.Expression).Returns(transactions.AsQueryable().Expression);
            mockTransactions.As<IQueryable<IQueryable<Transaction>>>().Setup(m => m.ElementType).Returns(transactions.AsQueryable().ElementType);
            mockTransactions.As<IQueryable<Transaction>>().Setup(m => m.GetEnumerator()).Returns(transactions.AsQueryable().GetEnumerator());

            mockTypes.As<IQueryable<IQueryable<Type>>>().Setup(m => m.Provider).Returns(types.AsQueryable().Provider);
            mockTypes.As<IQueryable<IQueryable<Type>>>().Setup(m => m.Expression).Returns(types.AsQueryable().Expression);
            mockTypes.As<IQueryable<IQueryable<Type>>>().Setup(m => m.ElementType).Returns(types.AsQueryable().ElementType);
            mockTypes.As<IQueryable<Type>>().Setup(m => m.GetEnumerator()).Returns(types.AsQueryable().GetEnumerator());

            mockCategories.As<IQueryable<IQueryable<Category>>>().Setup(m => m.Provider).Returns(categories.AsQueryable().Provider);
            mockCategories.As<IQueryable<IQueryable<Category>>>().Setup(m => m.Expression).Returns(categories.AsQueryable().Expression);
            mockCategories.As<IQueryable<IQueryable<Category>>>().Setup(m => m.ElementType).Returns(categories.AsQueryable().ElementType);
            mockCategories.As<IQueryable<Category>>().Setup(m => m.GetEnumerator()).Returns(categories.AsQueryable().GetEnumerator());

            var v = new MockContext();
            v.Categories = mockCategories.Object;
            v.Categories.AddRange(categories);
            v.Transactions = mockTransactions.Object;
            v.Transactions.AddRange(transactions);
            v.Types = mockTypes.Object;
            v.Types.AddRange(types);
            return v;
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Type> Types { get; set; }

        public int SaveChanges()
        {
            return 0;
        }
    }
}