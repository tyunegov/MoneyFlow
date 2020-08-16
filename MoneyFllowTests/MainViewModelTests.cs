using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyFllowControlLibrary.Model;
using Moq;
using System;

namespace MoneyFllow.Tests
{
    [TestClass()]
    public class MainViewModelTests
    {        
        [DataTestMethod]
        [DataRow("", 1)]
        [DataRow("test", 0)]
        [DataRow("", 0)]
        public void CanExecuteAddTransactionCommand_ReturnFalse(string category, int summ)
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
            Transaction transaction = new Transaction(new DateTime(), "test", "", 123);
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
            _transactionRepository.Setup(x=>x.Add(It.IsAny<Transaction>()));
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


        [TestMethod()]
        public void CanExecuteFilterTransactionCommand_ReturnTrue()
        {
            MainViewModel main = new MainViewModel();

            bool result = main.CanExecuteFilterTransactionCommand();
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void ExecuteFilterTransactionCommand_SelectedSortAndFilter()
        {
            Mock<ITransactionRepository> _transactionRepository = new Mock<ITransactionRepository>();
            MainViewModel main = new MainViewModel(_transactionRepository.Object);

            _transactionRepository.Setup(x => x.Sort(It.IsAny<SortTransaction>()));
            _transactionRepository.Setup(x => x.Filter(It.IsAny<TypeTransaction>()));

            main.ExecuteFilterTransactionCommand();
            _transactionRepository.VerifyAll();
        }
    }
}