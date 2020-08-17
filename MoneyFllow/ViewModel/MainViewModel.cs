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
        TransactionModel newTransaction, selectedTransaction;
        RelayCommand addCommand, deleteCommand, filterCommand;
        ObservableCollection<TransactionModel> transactions;
        TypeModel selectedFilterType;
        string selectedSort;

        public List<string> Sort 
        {
            get
            {
                return sortTransaction.GetAll();
            }
        }

        public ObservableCollection<TypeModel> Types
        {
            get
            {
                return typeTransaction.GetAll();
            }
        }

        public ObservableCollection<CategoryModel> Categories
        {
            get
            {
                return categoryRepository.GetByType(newTransaction.Type);
            }
        }

        public TypeModel SelectedFilterType
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

        public MainViewModel()
        {
            newTransaction = new TransactionModel();
            selectedFilterType = new TypeModel() { Title="123"};
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

        public TransactionModel NewTransaction 
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

        public ObservableCollection<TransactionModel> Transactions
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

        public TransactionModel SelectedTransaction
        {
            get 
            {
                return selectedTransaction; 
            }
            set
            {
                selectedTransaction = new TransactionModel();
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
                newTransaction.Type==null
                || newTransaction.Category==null
                || newTransaction.Summ == 0
                );
        }

        internal void ExecuteAddTransactionCommand()
        {
            transactionRepository.Add(newTransaction);
            NewTransaction = new TransactionModel();
        }
    }
}