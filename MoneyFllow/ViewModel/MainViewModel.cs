using Castle.Core.Internal;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MoneyFllow.Model;
using MoneyFllowControlLibrary;
using MoneyFllowControlLibrary.Context;
using MoneyFllowControlLibrary.Controller;
using MoneyFllowControlLibrary.Interface;
using MoneyFllowControlLibrary.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Type = MoneyFllowControlLibrary.Model.Type;

[assembly: InternalsVisibleTo("MoneyFllowTests")]
namespace MoneyFllow
{
    class MainViewModel : ViewModelBase
    {
        ObservableCollection<Transaction> transactions;
        ITransactionRepository transactionRepository;
        ITypeTransactionRepository typeTransaction;
        ICategoryRepository categoryRepository;
        Transaction newTransaction, selectedTransaction;
        RelayCommand addCommand, deleteCommand, filterCommand;
        Type selectedFilterType, typeForNewTransaction;
        DateTime newTransactionDate;
        Category categoryForNewTransaction;
        string selectedSort;

        public MainViewModel()
        {
            newTransaction = new Transaction();
            newTransactionDate = DateTime.Now;
            typeForNewTransaction = new Type();
            IApplicationContext context = new MockContext();
            selectedFilterType = new Type();
            categoryForNewTransaction = new Category();
            categoryRepository = new CategoryRepository();
            transactionRepository = new TransactionRepository();
            typeTransaction = new TypeRepository();
            deleteCommand = new RelayCommand(ExecuteDeleteTransactionCommand, CanExecuteDeleteTransactionCommand);
            //выбираем все транзакции для первичного отображения
            transactions = new ObservableCollection<Transaction>(transactionRepository.GetAll().ToList());
        }

        public MainViewModel(ITransactionRepository transaction)
        {
            transactionRepository = transaction;
        }

        public ObservableCollection<Type> Types
        {
            get
            {
                return new ObservableCollection<Type>(typeTransaction.GetAll().ToList());
            }
        }

        public ObservableCollection<Category> Categories
        {
            get
            {
                return categoryRepository.GetByTypeId(selectedFilterType.Id) as ObservableCollection<Category>;
            }
        }

        public Type SelectedFilterType
        {
            get
            {
                if (filterCommand != null)
                    filterCommand.RaiseCanExecuteChanged();
                return selectedFilterType;
            }
            set
            {
                selectedFilterType = value;
                RaisePropertyChanged("SelectedFilterType");
            }
        }

        public string SelectedSort
        {
            get
            {
                if (filterCommand != null)
                    filterCommand.RaiseCanExecuteChanged();
                return selectedSort;
            }
            set
            {
                selectedSort = value;
                RaisePropertyChanged("SelectedSort");
            }
        }

        public Transaction NewTransaction
        {
            get
            {
                if (addCommand != null)
                    addCommand.RaiseCanExecuteChanged();
                return newTransaction;
            }
            set
            {
                newTransaction = value;
                RaisePropertyChanged("NewTransaction");
            }
        }

        public Type TypeForNewTransaction
        {
            get
            {
                if (addCommand != null)
                    addCommand.RaiseCanExecuteChanged();
                return typeForNewTransaction;
            }
            set
            {
                typeForNewTransaction = value;
                RaisePropertyChanged("TypeForNewTransaction");
            }
        }

        public Category CategoryForNewTransaction
        {
            get
            {
                if (addCommand != null)
                    addCommand.RaiseCanExecuteChanged();
                return categoryForNewTransaction;
            }
            set
            {
                categoryForNewTransaction = value;
                RaisePropertyChanged("CategoryForNewTransaction");
            }
        }

        /// <summary>
        /// Выводит транзакции в таблицу
        /// </summary>
        public ObservableCollection<Transaction> Transactions
        {
            get
            {
                return transactions;
            }
        }

        public DateTime NewTransactionDate
        {
            get
            {
                return newTransactionDate;
            }
            set
            {
                newTransactionDate = value;
            }
        }

        public Transaction SelectedTransaction
        {
            get
            {
                return selectedTransaction;
            }
            set
            {
                selectedTransaction = new Transaction();
                selectedTransaction = value;
                deleteCommand.RaiseCanExecuteChanged();
            }
        }


        public ICommand AddTransaction
        {
            get
            {
                if (addCommand == null) addCommand = new RelayCommand(ExecuteAddTransactionCommand, CanExecuteAddTransactionCommand);
                return addCommand;
            }
        }

        public ICommand DeleteTransaction
        {
            get
            {
                if (deleteCommand == null) deleteCommand = new RelayCommand(ExecuteDeleteTransactionCommand, CanExecuteDeleteTransactionCommand);
                return deleteCommand;
            }

        }

        public ICommand ApplyFilterCommand
        {
            get
            {
                if (filterCommand == null) filterCommand = new RelayCommand(ExecuteFilterTransactionCommand, CanExecuteFilterTransactionCommand);
                return filterCommand;
            }
        }

        /// <summary>
        /// Фильтрует транзакции для вывода в таблице
        /// </summary>
        internal void ExecuteFilterTransactionCommand()
        {
            transactions = new ObservableCollection<Transaction>(transactionRepository.GetByTypeId(selectedFilterType.Id).ToList());
            RaisePropertyChanged("Transactions");
            //Transactions = transactionRepository.Sort(selectedSort);
        }

        internal bool CanExecuteFilterTransactionCommand()
        {
            return (
                !string.IsNullOrEmpty(selectedSort)
                || selectedFilterType != null
                );
        }

        internal bool CanExecuteDeleteTransactionCommand()
        {
            return SelectedTransaction != null;
        }

        internal void ExecuteDeleteTransactionCommand()
        {
            transactionRepository.Delete(newTransaction);
        }

        internal bool CanExecuteAddTransactionCommand()
        {
            return !(
                   categoryForNewTransaction.Name.IsNullOrEmpty()
                || newTransaction.Summ == 0
                );
        }

        internal void ExecuteAddTransactionCommand()
        {
            newTransaction.Category = categoryForNewTransaction;
            newTransaction.Date = newTransactionDate;
            transactionRepository.Add(newTransaction);
            NewTransaction = new Transaction();
            RaisePropertyChanged("Transactions");
        }
    }
}