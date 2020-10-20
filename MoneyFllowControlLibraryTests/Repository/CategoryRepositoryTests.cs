using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyFllowControlLibrary.Context;
using MoneyFllowControlLibrary.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoneyFllowControlLibrary.Controller.Tests
{
    [TestClass()]
    public class CategoryRepositoryTests
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
            CategoryRepository repository = new CategoryRepository(db);
            var categories = repository.GetAll();
            foreach (var v in categories)
            {
                Assert.IsTrue(v.Type.Id==v.TypeId, "incorrect count categories");
            }
            Assert.AreEqual(3, repository.GetAll().Count());
        }

        [TestMethod()]
        public void GetByTypeId()
        {
            CategoryRepository repository = new CategoryRepository(db);
            var category = repository.GetByTypeId(1);
            Assert.AreEqual(2, category.Count());
            foreach(var v in category)
            {
                Assert.AreEqual(1,v.TypeId);
            }
        }
    }
}