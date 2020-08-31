using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MoneyFllow.Model;
using MoneyFllowControlLibrary.Controller;
using MoneyFllowControlLibrary.Interface;
using MoneyFllowControlLibrary.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

[assembly: InternalsVisibleTo("MoneyFllowTests")]
namespace MoneyFllow
{
    class MainViewModel:ViewModelBase
    {
        ITransactionRepository transactionRepository;
        ITypeTransactionRepository typeTransaction;
        ICategoryRepository categoryRepository;
        ISortTransaction sortTransaction;
        Transaction newTransaction, selectedTransaction;
        RelayCommand addCommand, deleteCommand, filterCommand;
        ObservableCollection<Transaction> transactions;
        Type selectedFilterType;
        string selectedSort;

        public MainViewModel()
        {
            newTransaction = new Transaction();
            selectedFilterType = new Type() { Title = "123" };
            categoryRepository = new CategoryRepository();
            transactionRepository = new TransactionRepository();
            typeTransaction = new TypeRepository();
            sortTransaction = new SortRepository();
            deleteCommand = new RelayCommand(ExecuteDeleteTransactionCommand, CanExecuteDeleteTransactionCommand);
        }

        public MainViewModel(ITransactionRepository transaction)
        {
            transactionRepository = transaction;
        }

        public List<string> Sort 
        {
            get
            {
                return sortTransaction.GetAll();
            }
        }

        public ObservableCollection<Type> Types
        {
            get
            {
                return typeTransaction.GetAll();
            }
        }

        public ObservableCollection<Category> Categories
        {
            get;
            //{
            //    return categoryRepository.GetByType(newTransaction.Type);
            //}
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
                if(addCommand!=null)
                addCommand.RaiseCanExecuteChanged();
                return newTransaction;
            }
            set 
            {
                newTransaction = value;
                RaisePropertyChanged("NewTransaction");
        }}

        public ObservableCollection<Transaction> Transactions
        {
            get 
            {
                if (transactions == null) return transactionRepository.GetAll();
                else return transactions;
            }
            set
            {
                transactions = value;
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
                if(addCommand==null) addCommand = new RelayCommand(ExecuteAddTransactionCommand, CanExecuteAddTransactionCommand);
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
            Transactions = transactionRepository.Filter(SelectedFilterType);
            Transactions = transactionRepository.Sort(selectedSort);
        }

        internal bool CanExecuteFilterTransactionCommand()
        {
            return (
                !string.IsNullOrEmpty(selectedSort)
                || selectedFilterType!=null
                );
        }

        internal bool CanExecuteDeleteTransactionCommand()
        {
            return SelectedTransaction!=null;
        }

        internal void ExecuteDeleteTransactionCommand()
        {
            transactionRepository.Delete(newTransaction);
        }
                
        internal bool CanExecuteAddTransactionCommand()
        {
            return !(
             //   newTransaction.Type==null
                 newTransaction.Category==null
                || newTransaction.Summ == 0
                );
        }

        internal void ExecuteAddTransactionCommand()
        {
            transactionRepository.Add(newTransaction);
            NewTransaction = new Transaction();
        }
    }
}