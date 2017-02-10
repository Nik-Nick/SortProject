using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortProject
{
    public class Name
    {
        public string FirstName, LastName;

        public Name(string lastName, string firstName)
        {
            LastName = lastName;
            FirstName = firstName;
        }

        public string GetLastNameFirstName()
        {
            return LastName + ", " + FirstName;
        }
    }
}
