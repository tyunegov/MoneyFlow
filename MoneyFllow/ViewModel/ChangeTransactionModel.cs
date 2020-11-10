using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using MoneyFllowControlLibrary.Interface;
using MoneyFllowControlLibrary.Model;
using System;
using System.Windows.Input;
using Type = MoneyFllowControlLibrary.Model.Type;
using MoneyFllow.Model;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using MoneyFllowControlLibrary.Controller;
using System.Windows;
using MoneyFllow.View;

namespace MoneyFllow.ViewModel
{
     class ChangeTransactionModel:ViewModelBase
    {
        ITypeTransactionRepository typeRepository;
        ITransactionRepository transactionRepository;
        DateTime date;
        Type typeForChangeTransaction;
        Category categoryForChangeTransaction;
        Transaction transaction;
        RelayCommand changeCommand;

        public ChangeTransactionModel()
        {
            typeRepository = new TypeRepository();
            transactionRepository = new TransactionRepository();
        }

        public Transaction Transaction
        {
            get
            {
                if (transaction == null)
                {
                    transaction = new Transaction();
                    transaction.Date = DateTime.Today;
                }
                if (changeCommand != null)
                    changeCommand.RaiseCanExecuteChanged();
                return transaction;
            }
            set
            {
                transaction = value;
                RaisePropertyChanged("Transactions");
            }
        }


        public ICommand ChangeTransactionCmd
        {
            get
            {
                if (changeCommand == null) changeCommand = new RelayCommand(ExecuteChangeTransactionCommand, CanChangeTransactionCommand);
                return changeCommand;
            }
        }

        private bool CanChangeTransactionCommand()
        {
            return categoryForChangeTransaction==null?false:
                   (categoryForChangeTransaction.Id > 0
                && transaction.Summ != 0
                );
        }

        public DateTime Date
        {
            get
            {
                if (date == null) date = Transaction.Date;
                return date;
            }
            set
            {
                date = value;
                RaisePropertyChanged("Date");
            }
        }
        public Type TypeForChangeTransaction
        {
            get
            {
                if (typeForChangeTransaction == null) typeForChangeTransaction = Types.Where(x => x.Id == Transaction.Category.TypeId).FirstOrDefault();
                return typeForChangeTransaction;
            }
            set
            {
                typeForChangeTransaction = value;
                RaisePropertyChanged("TypeForChangeTransaction");
            }
        }

        public Category CategoryForChangeTransaction
        {
            get
            {
                if (categoryForChangeTransaction == null)
                {
                    categoryForChangeTransaction = TypeForChangeTransaction.Categories.Where(x => x.Id == Transaction.CategoryId).FirstOrDefault();
                }
                if (changeCommand != null) changeCommand.RaiseCanExecuteChanged();
                return categoryForChangeTransaction;
            }
            set
            {
                categoryForChangeTransaction = value;
                RaisePropertyChanged("CategoryForChangeTransaction");
            }
        }

        public ObservableCollection<Type> Types
        {
            get
            {
                var res = new ObservableCollection<Type>(typeRepository.GetAll().ToList());
                return res;
            }
        }

        internal void ExecuteChangeTransactionCommand()
        {
            transaction.Category = CategoryForChangeTransaction;
            transaction.CategoryId = CategoryForChangeTransaction.Id;
            transactionRepository.Change(transaction);
            foreach (Window window in App.Current.Windows)
            {
                if (window is ChangeTransaction)
                    window.DialogResult = true;
            }
        }
    }
}
