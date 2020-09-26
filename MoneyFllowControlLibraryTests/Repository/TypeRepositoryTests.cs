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
        MockContext db;

        [TestInitialize]
        public void Setup()
        {
            db = new MockContext();
        }

        [TestMethod()]
        public void GetAll()
        {
            TypeRepository repository = new TypeRepository(db);
            var types = repository.GetAll();
            foreach (var v in types)
            {
                Assert.IsTrue(v.Categories.All(p => p.TypeId == v.Id), "incorrect count categories");
            }
            Assert.AreEqual(3, repository.GetAll().Count());
        }
    }
}