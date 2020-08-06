using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MoneyFllow.Model
{
    class ClientRepository
    {
        static ObservableCollection<Client> _clients;
        public static ObservableCollection<Client> AllClients
        {
            get
            {
                if (_clients == null)
                    _clients = GenerateClientRepository();
                return _clients;
            }
        }

        private static ObservableCollection<Client> GenerateClientRepository()
        {
            var clients = new ObservableCollection<Client>();
            clients.Add(new Client("Name1", "Last1"));
            clients.Add(new Client("Name2", "Last2"));
            return clients;
        }
    }
}
