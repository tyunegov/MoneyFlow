using Castle.Core.Internal;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MoneyFllow.Model;
using MoneyFllowControlLibrary;
using MoneyFllowControlLibrary.Context;
using MoneyFllowControlLibrary.Controller;
using MoneyFllowControlLibrary.Interface;
using MoneyFllowControlLibrary.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

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
        string selectedSort;

        public MainViewModel()
        {
            newTransaction = new Transaction();
            typeForNewTransaction = new Type();
            IApplicationContext context = new MockContext();
            selectedFilterType = new Type();
            categoryForNewTransaction = new Category();
            categoryRepository = new CategoryRepository(context);
            transactionRepository = new TransactionRepository(context);
            typeTransaction = new TypeRepository(context);
            deleteCommand = new RelayCommand(ExecuteDeleteTransactionCommand, CanExecuteDeleteTransactionCommand);
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

        public ObservableCollection<Transaction> Transactions
        {
            get
            {
                if (SelectedFilterType.Id == 0)
                {
                    transactions = new ObservableCollection<Transaction>(transactionRepository.GetAll().ToList());

                }
                else transactions = new ObservableCollection<Transaction>(transactionRepository.GetByTypeId(selectedFilterType.Id).ToList());
                return transactions;
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

        internal void ExecuteFilterTransactionCommand()
        {
            //Transactions = transactionRepository.Filter(SelectedFilterType);
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
            transactionRepository.Add(newTransaction);
            NewTransaction = new Transaction();
            RaisePropertyChanged("Transactions");
        }
    }
}