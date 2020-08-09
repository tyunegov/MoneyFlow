using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyFllow.Model;
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
    }
}