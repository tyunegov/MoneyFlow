using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyFllowControlLibrary.Model;
using Moq;
using System;
using System.Collections.Generic;

namespace MoneyFllow.Tests
{
    [TestClass()]
    public class MainViewModelTests
    {
        public static IEnumerable<object[]> GetDataAddTransaction()
        {
            yield return new object[] {null, new CategoryModel(), 100m };
            yield return new object[] {new TypeModel(), null, 123m };
            yield return new object[] { new TypeModel(), new CategoryModel(), 0m};
            yield return new object[] {null, null, 0m };
        }

        [DataTestMethod]
        [DynamicData(nameof(GetDataAddTransaction), DynamicDataSourceType.Method)]
        public void CanExecuteAddTransactionCommand_ReturnFalse(TypeModel type, CategoryModel category, decimal summ)
        {
            TransactionModel transaction = new TransactionModel()
            {
                Type = type,
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
            TransactionModel transaction = new TransactionModel()
            {
                Type = new TypeModel(),
                Category = new CategoryModel()
                {
                    Title = "test",
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
            _transactionRepository.Setup(x=>x.Add(It.IsAny<TransactionModel>()));
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
            main.SelectedTransaction = new TransactionModel();
            bool result = main.CanExecuteDeleteTransactionCommand();
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void ExecuteDeleteTransactionCommand()
        {
            Mock<ITransactionRepository> _transactionRepository = new Mock<ITransactionRepository>();
            MainViewModel main = new MainViewModel(_transactionRepository.Object);
            _transactionRepository.Setup(x => x.Delete(It.IsAny<TransactionModel>()));
            main.ExecuteDeleteTransactionCommand();
            _transactionRepository.VerifyAll();
        }


        public static IEnumerable<object[]> GetDataFilterTransaction()
        {
            yield return new object[] {"123", null };
            yield return new object[] {"", new TypeModel() };
            yield return new object[] { "123", new TypeModel() };

        }

        [DataTestMethod]
        [DynamicData(nameof(GetDataFilterTransaction), DynamicDataSourceType.Method)]
        public void CanExecuteFilterTransactionCommand_ReturnTrue(string sort, TypeModel type)
        {
            MainViewModel main = new MainViewModel();
            main.SelectedSort = sort;
            main.SelectedFilterType = type;

            bool result = main.CanExecuteFilterTransactionCommand();
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void ExecuteFilterTransactionCommand_SelectedSortAndFilter()
        {
            Mock<ITransactionRepository> _transactionRepository = new Mock<ITransactionRepository>();
            MainViewModel main = new MainViewModel(_transactionRepository.Object);

            _transactionRepository.Setup(x => x.Sort(It.IsAny<string>()));
            _transactionRepository.Setup(x => x.Filter(It.IsAny<TypeModel>()));

            main.ExecuteFilterTransactionCommand();
            _transactionRepository.VerifyAll();
        }
    }
}