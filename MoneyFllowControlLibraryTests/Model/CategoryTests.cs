using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MoneyFllowControlLibrary.Model.Tests
{
    [TestClass()]
    public class CategoryTests
    {
        [TestMethod()]
        public void Equals_ReturnTrue()
        {
            Category expected = new Category() { Id = 1, Name = "cat", Transactions = new List<Transaction>(), TypeId = 1, Type = new Type() };
            Category actual = new Category() { Id = 1, Name = "cat", Transactions = new List<Transaction>(), TypeId = 1, Type = new Type() };
            Assert.AreEqual(expected, actual);
        }

        public static IEnumerable<object[]> GetIncorrectCategory()
        {
            yield return new object[] { new Category() { Id = 0, Name = "cat", Transactions = new List<Transaction>() { new Transaction(), new Transaction() }, TypeId = 1, Type = new Type() { Id = 1 } }, "incorrect Id" };
            yield return new object[] { new Category() { Id = 1, Name = "", Transactions = new List<Transaction>() { new Transaction(), new Transaction() }, TypeId = 1, Type = new Type() { Id = 1 } }, "incorrect Title" };
            yield return new object[] { new Category() { Id = 1, Name = "cat", Transactions = new List<Transaction>() { new Transaction() }, TypeId = 1, Type = new Type() { Id = 1 } }, "incorrect Transactions" };
            yield return new object[] { new Category() { Id = 1, Name = "cat", Transactions = new List<Transaction>() { new Transaction(), new Transaction() }, TypeId = 0, Type = new Type() { Id = 1 } }, "incorrect TypeId" };
            yield return new object[] { new Category() { Id = 1, Name = "cat", Transactions = new List<Transaction>() { new Transaction(), new Transaction() }, TypeId = 1, Type = new Type() { Id = 100 } }, "incorrect Type" };

        }

        [DataTestMethod]
        [DynamicData(nameof(GetIncorrectCategory), DynamicDataSourceType.Method)]
        public void Equals_ReturnFalse(Category incorrectCategory, string message)
        {
            Category expected = new Category() { Id = 1, Name = "cat", Transactions = new List<Transaction>() { new Transaction(), new Transaction() }, TypeId = 1, Type = new Type() { Id = 1 } };
            Assert.AreNotEqual(expected, incorrectCategory, message);
        }
    }
}