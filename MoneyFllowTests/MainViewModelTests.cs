using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyFllowControlLibrary.Model;
using Moq;
using System.Collections.Generic;

namespace MoneyFllow.Tests
{
    [TestClass()]
    public class MainViewModelTests
    {
        public static IEnumerable<object[]> GetDataAddTransaction()
        {
            yield return new object[] { null, 123m };
            yield return new object[] { new Category(), 0m };
        }

        [DataTestMethod]
        [DynamicData(nameof(GetDataAddTransaction), DynamicDataSourceType.Method)]
        public void CanExecuteAddTransactionCommand_ReturnFalse(Category category, decimal summ)
        {
            Transaction transaction = new Transaction()
            {
                Category = category,
                Summ = summ,
            };
            MainViewModel main = new MainViewModel();
            main.NewTransaction = transaction;
            bool result = main.CanExecuteAddTransactionCommand();
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CanExecuteAddTransactionCommand_ReturnTrue()
        {
            Transaction transaction = new Transaction()
            {
                Category = new Category()
                {
                    Id = 1,
                    Name = "test",
                },
                Summ = 123,
            };
            MainViewModel main = new MainViewModel();
            main.NewTransaction = transaction;
            bool result = main.CanExecuteAddTransactionCommand();
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void ExecuteAddTransactionCommand()
        {
            Mock<ITransactionRepository> _transactionRepository = new Mock<ITransactionRepository>();
            MainViewModel main = new MainViewModel(_transactionRepository.Object);
            _transactionRepository.Setup(x => x.Add(It.IsAny<Transaction>()));
            main.ExecuteAddTransactionCommand();
            _transactionRepository.VerifyAll();
        }

        [TestMethod()]
        public void CanExecuteDeleteTransactionCommand_ReturnFalse()
        {
            MainViewModel main = new MainViewModel();
            main.SelectedTransaction = null;
            bool result = main.CanExecuteDeleteTransactionCommand();
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void CanExecuteDeleteTransactionCommand_ReturnTrue()
        {
            MainViewModel main = new MainViewModel();
            main.SelectedTransaction = new Transaction();
            bool result = main.CanExecuteDeleteTransactionCommand();
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void ExecuteDeleteTransactionCommand()
        {
            Mock<ITransactionRepository> _transactionRepository = new Mock<ITransactionRepository>();
            MainViewModel main = new MainViewModel(_transactionRepository.Object);
            _transactionRepository.Setup(x => x.Delete(It.IsAny<Transaction>()));
            main.ExecuteDeleteTransactionCommand();
            _transactionRepository.VerifyAll();
        }


        public static IEnumerable<object[]> GetDataFilterTransaction()
        {
            yield return new object[] { "123", null };
            yield return new object[] { "", new MoneyFllowControlLibrary.Model.Type() };
            yield return new object[] { "123", new MoneyFllowControlLibrary.Model.Type() };

        }

        [DataTestMethod]
        [DynamicData(nameof(GetDataFilterTransaction), DynamicDataSourceType.Method)]
        public void CanExecuteFilterTransactionCommand_ReturnTrue(string sort, MoneyFllowControlLibrary.Model.Type type)
        {
            MainViewModel main = new MainViewModel();
          //  main.SelectedSort = sort;
            main.SelectedFilterType = type;

            bool result = main.CanExecuteFilterTransactionCommand();
            Assert.IsTrue(result);
        }
    }
}