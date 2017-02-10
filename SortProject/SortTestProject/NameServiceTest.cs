using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SortProject.Interfaces;
using Autofac;
using SortProject.Implementations;
using SortProject;
using System.Collections.Generic;
using System.Linq;

namespace SortTestProject
{

    [TestClass]
    public class NameServiceTest
    {
        private IContainer Container {get; set;}

        private INameService _nameService { get; set; }


        [TestInitialize]
        public void Initialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<NameService>().As<INameService>();
            Container = builder.Build();

            using(var scope = Container.BeginLifetimeScope())
            {
                _nameService = scope.Resolve<INameService>();
            }

        }

        [TestMethod]
        public void SplitLastNameFirtName_GoodName_GetSplitName()
        {
            // Arrange
            var expected = new string[2] { "ABC", " DEF" };

            // Act
            var result =  _nameService.SplitLastNameFirstName("ABC, DEF");
            
            // Assert
            Assert.AreEqual(expected[0], result[0]);
            Assert.AreEqual(expected[1], result[1]);
            Assert.AreEqual(expected.Length, result.Length);
        }

        [TestMethod]
        public void IsValidName_aZ_True()
        {
            //Arrange
            var name = new string[2] { "aBC", "zOr" };

            // Act
            var result = _nameService.IsValidName(name);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidName_aZspace_True()
        {
            //Arrange
            var name = new string[2] { " AbC esdd", "ZoR " };

            // Act
            var result = _nameService.IsValidName(name);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidName_09_False()
        {
            //Arrange
            var name = new string[2] { "05", "19" };

            // Act
            var result = _nameService.IsValidName(name);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidName_aZ09_False()
        {
            //Arrange
            var name = new string[2] { "ee2", "zOr" };

            // Act
            var result = _nameService.IsValidName(name);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidName_Empty_False()
        {
            //Arrange
            var name = new string[2] { "", "" };

            // Act
            var result = _nameService.IsValidName(name);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidName_Space_False()
        {
            //Arrange
            var name = new string[2] { " ", " " };

            // Act
            var result = _nameService.IsValidName(name);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateName_GoodName_GetName()
        {
            //Arrange
            var name = new string[2] { "Dupuskull", "Nikom" };

            // Act
            var result = _nameService.CreateName(name);

            // Assert
            Assert.AreEqual("Nikom", result.FirstName);
            Assert.AreEqual("Dupuskull", result.LastName);
            Assert.AreEqual("Dupuskull, Nikom", result.GetLastNameFirstName());
        }

        [TestMethod]
        public void CreateName_GoodNameWithSpace_GetName()
        {
            //Arrange
            var name = new string[2] { "Dupuskull ", " Nikom" };

            // Act
            var result = _nameService.CreateName(name);

            // Assert
            Assert.AreEqual("Nikom", result.FirstName);
            Assert.AreEqual("Dupuskull", result.LastName);
            Assert.AreEqual("Dupuskull, Nikom", result.GetLastNameFirstName());
        }

        [TestMethod]
        public void CreateName_GoodNameWithSpace2_GetName()
        {
            //Arrange
            var name = new string[2] { "Dupu skull ", " Ni kom" };

            // Act
            var result = _nameService.CreateName(name);

            // Assert
            Assert.AreEqual("Ni kom", result.FirstName);
            Assert.AreEqual("Dupu skull", result.LastName);
            Assert.AreEqual("Dupu skull, Ni kom", result.GetLastNameFirstName());
        }

        [TestMethod]
        public void CreateName_MissingSection_GetNull()
        {
            //Arrange
            var name = new string[1] { "Dupuskull "};

            // Act
            var result = _nameService.CreateName(name);

            // Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void CreateName_MoreSection_GetNull()
        {
            //Arrange
            var name = new string[3] { "Dupuskull ", "Nik", "e" };

            // Act
            var result = _nameService.CreateName(name);

            // Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void CreateName_MoreSectionWithEmpty_GetNull()
        {
            //Arrange
            var name = new string[3] { "Dupuskull ", "Nik", "" };

            // Act
            var result = _nameService.CreateName(name);

            // Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void CreateNameList_GoodNames_GetNameList()
        {
            //Arrange
            var dataRows = new List<string>();
            dataRows.Add("BAKER, THEODORE");
            dataRows.Add("KENT, MADISON");
            dataRows.Add("SMITH, ANDREW");

            // Act
            var result = _nameService.CreateNameList(dataRows);

            // Assert
            Assert.IsTrue(result.Any(x => x.FirstName == "THEODORE" && x.LastName == "BAKER"));
            Assert.IsTrue(result.Any(x => x.FirstName == "MADISON" && x.LastName == "KENT"));
            Assert.IsTrue(result.Any(x => x.FirstName == "ANDREW" && x.LastName == "SMITH"));
            Assert.IsTrue(result.Count == 3);
        }

        [TestMethod]
        public void CreateNameList_WithBadName_GetSomeNameList()
        {
            //Arrange
            var dataRows = new List<string>();
            dataRows.Add("BAKER, THEODORE");
            dataRows.Add("KENT, MAD7SON");
            dataRows.Add("SMITH, ANDREW");

            // Act
            var result = _nameService.CreateNameList(dataRows);

            // Assert
            Assert.IsTrue(result.Any(x => x.FirstName == "THEODORE" && x.LastName == "BAKER"));
            Assert.IsFalse(result.Any(x => x.FirstName == "MAD7SON" && x.LastName == "KENT"));
            Assert.IsTrue(result.Any(x => x.FirstName == "ANDREW" && x.LastName == "SMITH"));
            Assert.IsTrue(result.Count == 2);
        }

        [TestMethod]
        public void SortedNameList_GoodNames_GetSortedList()
        {
            //Arrange
            var nameList = new List<Name>();
            nameList.Add( new Name("KENT", "MADISON"));
            nameList.Add( new Name("SMITH", "ANDREW"));
            nameList.Add( new Name("BAKER", "THEODORE"));
            nameList.Add(new Name("BAKER", "ROGER"));
            var expected = new List<Name>();
            expected.Add(new Name("BAKER", "ROGER"));
            expected.Add(new Name("BAKER", "THEODORE"));
            expected.Add(new Name("KENT", "MADISON"));
            expected.Add(new Name("SMITH", "ANDREW"));

            // Act
            var result = _nameService.SortedNameList(nameList);

            // Assert
            for(var i = 0; i < nameList.Count; i++)
            {
                Assert.AreEqual(expected[i].FirstName, result[i].FirstName);
                Assert.AreEqual(expected[i].LastName, result[i].LastName);
            }
            Assert.IsTrue(expected.Count == result.Count);
            
        }
    }
}
