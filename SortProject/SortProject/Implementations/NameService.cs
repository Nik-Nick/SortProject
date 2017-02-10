using SortProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SortProject.Implementations
{
    public class NameService: INameService
    {
        const int NAME_SECTIONS = 2;
        const int LASTNAME_SECTION = 0;
        const int FIRSTNAME_SECTION = 1;

        /// <summary>
        /// Split fullname
        /// </summary>
        /// <param name="name"> lastName, firstName</param>
        /// <returns>lastName and firstName</returns>
        public string[] SplitLastNameFirstName(string name)
        {
            var lastNameFirstName = name.Trim().Split(',');
            if (lastNameFirstName.Length != NAME_SECTIONS)
            {
                return null;
            }
            return lastNameFirstName;
        }

        /// <summary>
        /// Check name contains letters & allow space
        /// </summary>
        /// <param name="name">part of name</param>
        /// <returns>Is match</returns>
        public bool IsValidName(string[] name)
        {
            if(name == null)
            {
                return false;
            }

            foreach(var namePart in name)
            {
                if(!Regex.IsMatch(namePart, @"^[a-zA-Z ]+$") || namePart.Trim().Length == 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Create fullname
        /// </summary>
        /// <param name="lastNameFirstName">[lastName, firstname]</param>
        /// <returns>lastName, firstName</returns>
        public Name CreateName(string[] lastNameFirstName)
        {
            if(lastNameFirstName.Length != NAME_SECTIONS)
            {
                return null;
            }
            return new Name(lastNameFirstName[LASTNAME_SECTION].Trim(), lastNameFirstName[FIRSTNAME_SECTION].Trim());
        }

        /// <summary>
        /// Add valid name to name list
        /// </summary>
        /// <param name="lines">names</param>
        /// <returns>name list</returns>
        public List<Name> CreateNameList(List<string> lines)
        {
            var nameList = new List<Name>();
            foreach (var line in lines)
            {
                var lastNameFirstName = SplitLastNameFirstName(line);
                if (IsValidName(lastNameFirstName))
                {
                    nameList.Add(CreateName(lastNameFirstName));
                }
            }

            return nameList;
        }

        /// <summary>
        /// Sort names by lastName then firstName
        /// </summary>
        /// <param name="nameList">name list</param>
        /// <returns>sorted name list</returns>
        public List<Name> SortedNameList(List<Name> nameList)
        {
            return nameList.OrderBy(n => n.LastName).ThenBy(n => n.FirstName).ToList();
        }
    }
}
