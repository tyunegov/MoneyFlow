using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyFllow.Model
{
    class Client
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Client() { }

        public Client(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }
    }
}
