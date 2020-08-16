using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MoneyFllow.Model;
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
        public ITransactionRepository transactionRepository;
        public ITypeTransactionRepository typeTransaction;
        public ISortTransaction sortTransaction;
        Transaction newTransaction, selectedTransaction;
        RelayCommand addCommand, deleteCommand, filterCommand;
        ObservableCollection<Transaction> transactions;
        SortTransaction selectedSort;
        TypeTransaction selectedFilterType;
        public List<string> Sort 
        {
            get
            {
                return sortTransaction.GetAll();
            }
        }

        public ObservableCollection<string> Types
        {
            get
            {
                return typeTransaction.GetAll();
            }
        }

        public SortTransaction SelectedSort { get => selectedSort; set => selectedSort = value; }
        public TypeTransaction SelectedFilterType { get => selectedFilterType; set => selectedFilterType = value; }

        public MainViewModel()
        {
            newTransaction = new Transaction();
            transactionRepository = new TransactionRepository();
            typeTransaction = new TypeTransaction();
            sortTransaction = new SortTransaction();
            deleteCommand = new RelayCommand(ExecuteDeleteTransactionCommand, CanExecuteDeleteTransactionCommand);
        }

        public MainViewModel(ITransactionRepository transaction)
        {
            transactionRepository = transaction;
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
            Transactions = transactionRepository.Sort(SelectedSort);
        }

        internal bool CanExecuteFilterTransactionCommand()
        {
            return true;
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
                string.IsNullOrEmpty(newTransaction.Category)
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