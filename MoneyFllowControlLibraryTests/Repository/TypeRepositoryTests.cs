using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyFllowControlLibrary.Context;
using MoneyFllowControlLibrary.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoneyFllowControlLibrary.Model.Tests
{
    [TestClass()]
    public class TypeRepositoryTests
    {
        TypeRepository repository;
        [TestInitialize]
        public void Setup()
        {
            IApplicationContext db = new MockContext().Create();
         //   db.Create();
            repository = new TypeRepository(db);
        }

        [TestMethod()]
        public void GetAll()
        {
            var types = repository.GetAll();
            foreach (var v in types)
            {
                Assert.IsTrue(v.Categories.All(p => p.TypeId == v.Id), "incorrect count categories");
            }
            Assert.AreEqual(3, repository.GetAll().Count());
        }
    }
}