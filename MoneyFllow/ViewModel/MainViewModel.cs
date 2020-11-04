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
        Category categoryForNewTransaction;
        DateTime dateStart, dateEnd;
        string selectedSort;

        public MainViewModel()
        {            
            typeForNewTransaction = new Type();
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
                var result = new ObservableCollection<Type>(typeTransaction.GetAll().ToList());
                return result;
            }
        }

        public ObservableCollection<Category> Categories
        {
            get
            {
                return categoryRepository.GetByTypeId(selectedFilterType.Id) as ObservableCollection<Category>;
            }
        }


        #region FilterTransactions
        public Type SelectedFilterType
        {
            get
            {
                selectedFilterType = selectedFilterType == null ? FilterTypes.Last() : selectedFilterType;
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

        public ObservableCollection<Type> FilterTypes
        {
            get
            {

                var result = new ObservableCollection<Type>(typeTransaction.GetAll().ToList());
                result.Add(new Type() { Id = 0, Name = "Все" });
                /// Добавить параметр все
                return result;
            }
        }

        public DateTime DateStart
        {
            get
            {
                if ((int)dateStart.CompareTo(new DateTime())==0)
                    dateStart = new DateTime(2000,01,01);
                return dateStart;
            }
            set
            {
                dateStart = value;
                RaisePropertyChanged("DateStart");
            }
        }

        public DateTime DateEnd
        {
            get
            {
                if ((int)dateEnd.CompareTo(new DateTime()) == 0)
                    dateEnd = DateTime.Now;
                return dateEnd;
            }
            set
            {
                dateEnd = value;
                RaisePropertyChanged("DateEnd");
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
            Transactions = new ObservableCollection<Transaction>(transactionRepository.Filter(selectedFilterType.Id, dateStart, dateEnd).ToList());
        }

        internal bool CanExecuteFilterTransactionCommand()
        {
            return (
                !string.IsNullOrEmpty(selectedSort)
                || selectedFilterType != null
                );
        } 
        #endregion

        /// <summary>
        /// Выводит транзакции в таблицу
        /// </summary>
        public ObservableCollection<Transaction> Transactions
        {
            get
            {
                return transactions;
            }
            set
            {
                transactions = value;
                RaisePropertyChanged("Transactions");
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

        public ICommand DeleteTransaction
        {
            get
            {
                if (deleteCommand == null) deleteCommand = new RelayCommand(ExecuteDeleteTransactionCommand, CanExecuteDeleteTransactionCommand);
                return deleteCommand;
            }

        }

        internal bool CanExecuteDeleteTransactionCommand()
        {
            return SelectedTransaction != null;
        }

        internal void ExecuteDeleteTransactionCommand()
        {
            transactionRepository.Delete(newTransaction);
        }

        #region Add transaction

        public Transaction NewTransaction
        {
            get
            {
                if (newTransaction == null)
                {
                    newTransaction = new Transaction();
                    newTransaction.Date = DateTime.Today;
                }
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
                if(categoryForNewTransaction==null) categoryForNewTransaction = new Category();
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

        public ICommand AddTransaction
        {
            get
            {
                if (addCommand == null) addCommand = new RelayCommand(ExecuteAddTransactionCommand, CanExecuteAddTransactionCommand);
                return addCommand;
            }
        }

        internal bool CanExecuteAddTransactionCommand()
        {
            return (
                   categoryForNewTransaction.Id>0
                && newTransaction.Summ != 0
                );
        }

        internal void ExecuteAddTransactionCommand()
        {
            newTransaction.Category = categoryForNewTransaction;
            int resultAdd = transactionRepository.Add(newTransaction);
            if (resultAdd>0) Transactions.Add(newTransaction);

            RaisePropertyChanged("Transactions");

            NewTransaction = new Transaction();
        }
        #endregion
    }
}