using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MoneyFllow.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MoneyFllow
{
    internal class MainViewModel:ViewModelBase
    {
        private Transaction _currentTransaction;

        public MainViewModel()
        {
            command = new RelayCommand(ExecuteAddClientCommand, CanExecuteAddClientCommand);
        }
        public Transaction CurrentTransaction 
        {
            get
            {
                if (_currentTransaction == null)
                    _currentTransaction = new Transaction();
                command.RaiseCanExecuteChanged();
                return _currentTransaction;
            }
            set 
            {
                _currentTransaction = value;
                RaisePropertyChanged("CurrentTransaction");
        }}

        public ObservableCollection<Transaction> Transaction
        {
            get { return TransactionRepository.AllTransaction; }
        }

        RelayCommand command;
        public ICommand AddTransaction
        {
            get
            {
                if (command == null)
                    command = new RelayCommand(ExecuteAddClientCommand, CanExecuteAddClientCommand);
                
                
                return command;
            }

        }



        private bool CanExecuteAddClientCommand()
        {
            bool b=true;
            // b = _currentTransaction == null ? true : false;
            if (string.IsNullOrEmpty(_currentTransaction.Category) ||
                _currentTransaction.Summ==0)
                return false;
            else return true;
        }

        private void ExecuteAddClientCommand()
        {
            Transaction.Add(_currentTransaction);
            CurrentTransaction = null;
        }
    }
}