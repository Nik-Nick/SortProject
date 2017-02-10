using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortProject.Interfaces
{
    public interface INameService
    {
        string[] SplitLastNameFirstName(string name);

        bool IsValidName(string[] name);

        Name CreateName(string[] lastNameFirstName);

        List<Name> CreateNameList(List<string> lines);

        List<Name> SortedNameList(List<Name> nameList);

    }
}
