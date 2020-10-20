using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MoneyFllowControlLibrary.Model.Tests
{
    [TestClass()]
    public class TypeTests
    {
        [TestMethod()]
        public void Equals_ReturnTrue()
        {
            Type type1 = new Type() { Id = 1, Name = "123", Categories = new List<Category>() };
            Type type2 = new Type() { Id = 1, Name = "123", Categories = new List<Category>() };

            Assert.AreEqual(type1, type2);
        }

        public static IEnumerable<object[]> GetIncorrectTypes()
        {
            yield return new object[] { new Type() { Id = 0, Name = "123", Categories = new List<Category>() { new Category() { Id = 1, Name = "Category1" }, new Category() { Id = 2, Name = "Category2" } } }, "Incorrect Id" };
            yield return new object[] { new Type() { Id = 1, Name = "", Categories = new List<Category>() { new Category() { Id = 1, Name = "Category1" }, new Category() { Id = 2, Name = "Category2" } } }, "Incorrect Title" };
            yield return new object[] { new Type() { Id = 1, Name = "123", Categories = new List<Category>() { new Category() { Id = 1, Name = "Category1" } } }, "Incorrect Category" };
        }

        [DataTestMethod]
        [DynamicData(nameof(GetIncorrectTypes), DynamicDataSourceType.Method)]
        public void Equals_ReturnFalse(Type incorrectType, string message)
        {
            Type expected = new Type() { Id = 1, Name = "123", Categories = new List<Category>() { new Category() { Id = 1, Name = "Category1" }, new Category() { Id = 2, Name = "Category2" } } };

            Assert.AreNotEqual(expected, incorrectType, message);
        }
    }
}