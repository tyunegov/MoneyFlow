using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MoneyFllowControlLibrary.Model.Tests
{
    [TestClass()]
    public class TransactionTests
    {
        [TestMethod()]
        public void Equals_returnTrue()
        {
            Transaction expected = new Transaction() { Id = 1, Category = new Category() { Id = 1, Transactions = new List<Transaction>() }, CategoryId = 2, Date = new DateTime(), Description = "desc", Summ = 100 };
            Transaction actual = new Transaction() { Id = 1, Category = new Category() { Id = 1, Transactions = new List<Transaction>() }, CategoryId = 2, Date = new DateTime(), Description = "desc", Summ = 100 };
            Assert.AreEqual(expected, actual);
        }

        public static IEnumerable<object[]> GetIncorrectTransaction()
        {
            yield return new object[] { new Transaction() { Id = 100, Category = new Category() { Id = 5 }, CategoryId = 2, Date = new DateTime(), Description = "desc", Summ = 100 }, "Incorrect Id" };
            yield return new object[] { new Transaction() { Id = 1, Category = new Category() { Id = 5 }, CategoryId = 2, Date = new DateTime(), Description = "desc", Summ = 100 }, "Incorrect Category" };
            yield return new object[] { new Transaction() { Id = 1, Category = new Category() { Id = 1 }, CategoryId = 0, Date = new DateTime(), Description = "desc", Summ = 100 }, "Incorrect CategoryId" };
            yield return new object[] { new Transaction() { Id = 1, Category = new Category() { Id = 1 }, CategoryId = 2, Date = new DateTime(1972, 01, 01), Description = "desc", Summ = 100 }, "Incorrect Date" };
            yield return new object[] { new Transaction() { Id = 1, Category = new Category() { Id = 1 }, CategoryId = 2, Date = new DateTime(), Description = "", Summ = 100 }, "Incorrect Description" };
            yield return new object[] { new Transaction() { Id = 1, Category = new Category() { Id = 1 }, CategoryId = 2, Date = new DateTime(), Description = "desc", Summ = 0 }, "Incorrect Summ" };
        }

        [DataTestMethod]
        [DynamicData(nameof(GetIncorrectTransaction), DynamicDataSourceType.Method)]
        public void Equals_ReturnFalse(Transaction incorrectType, string message)
        {
            Transaction expected = new Transaction() { Id = 1, Category = new Category() { Id = 1 }, CategoryId = 2, Date = new DateTime(), Description = "desc", Summ = 100 };
            Assert.IsFalse(expected.Equals(incorrectType), message);
        }

        [TestMethod()]
        public void Equals_OnlyId_ReturnFalse()
        {
            Transaction expected = new Transaction() { Id = 1 };
            Transaction incorrect = new Transaction() { Id = 0 };

            Assert.IsFalse(expected.Equals(incorrect));
        }

        [TestMethod()]
        public void Equals_OnlyCategory_ReturnFalse()
        {
            Transaction expected = new Transaction() { Category = new Category() { Id = 1 } };
            Transaction incorrect = new Transaction() { Category = new Category() { Id = 10 } };

            Assert.IsFalse(expected.Equals(incorrect));
        }

        [TestMethod()]
        public void Equals_OnlyCategoryId_ReturnFalse()
        {
            Transaction expected = new Transaction() { CategoryId = 1 };
            Transaction incorrect = new Transaction() { CategoryId = 0 };

            Assert.IsFalse(expected.Equals(incorrect));
        }

        [TestMethod()]
        public void Equals_OnlyDescriptionId_ReturnFalse()
        {
            Transaction expected = new Transaction() { Description = "Description" };
            Transaction incorrect = new Transaction() { Description = "" };

            Assert.IsFalse(expected.Equals(incorrect));
        }

        [TestMethod()]
        public void Equals_OnlySumm_ReturnFalse()
        {
            Transaction expected = new Transaction() { Summ = 1 };
            Transaction incorrect = new Transaction() { Summ = 0 };

            Assert.IsFalse(expected.Equals(incorrect));
        }

        [TestMethod()]
        public void Equals_OnlyDate_ReturnFalse()
        {
            Transaction expected = new Transaction() { Date = new DateTime() };
            Transaction incorrect = new Transaction() { Date = new DateTime(1972, 01, 01) };

            Assert.IsFalse(expected.Equals(incorrect));
        }
    }
}