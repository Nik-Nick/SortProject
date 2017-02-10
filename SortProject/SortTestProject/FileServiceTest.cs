using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using SortProject.Interfaces;
using SortProject.Implementations;
using SortProject;
using System.Diagnostics;

namespace SortTestProject
{
    /// <summary>
    /// Summary description for FileServiceTest
    /// </summary>
    [TestClass]
    public class FileServiceTest
    {
        private IContainer Container { get; set; }

        private IOutput _output { get; set; }

        private INameService _nameService { get; set; }

        private IFileService _fileService { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Output>().As<IOutput>();
            builder.RegisterType<NameService>().As<INameService>();
            builder.RegisterType<FileService>().As<IFileService>();
            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                _output = scope.Resolve<IOutput>();
                _nameService = scope.Resolve<INameService>();
                _fileService = scope.Resolve<IFileService>();
            }

           
        }

        [TestMethod]
        public void CreateSortedTextFile_GoodFile_GetNewSortedFile()
        {

            // Arrange
            var unsortedData = new List<string>();
            unsortedData.Add("EFG, DEF");
            unsortedData.Add("ABC, DEF");
            unsortedData.Add("ABC, EFG");

            var sortedData = new List<string>();
            sortedData.Add("ABC, DEF");
            sortedData.Add("ABC, EFG");
            sortedData.Add("EFG, DEF");

            var unsorted = new TextFile { FileName = "names", FilePath = "a/b", DataRows = unsortedData};

            // Act
            var result = _fileService.CreateSortedTextFile(unsorted);

            // Assert
            Assert.AreEqual("names-sorted", result.FileName);
            Assert.AreEqual("a/b", result.FilePath);
            for(var i =0; i< sortedData.Count; i++)
            {
                Assert.AreEqual(sortedData[i], result.DataRows[i]);
            }
            Assert.IsTrue(result.DataRows.Count == sortedData.Count);
        }

        [TestMethod]
        public void CreateSortedTextFile_noFile_GetNewSortedFile()
        {
            // Arrange
            var unsorted = new TextFile();

            // Act
            var result = _fileService.CreateSortedTextFile(unsorted);

            // Assert
            Assert.AreEqual(unsorted, result);
        }

        [TestMethod]
        public void CreateSortedTextFile_noFileName_GetNewSortedFile()
        {
            // Arrange
            var unsorted = new TextFile { FileName="names", DataRows = new List<string>()};

            // Act
            var result = _fileService.CreateSortedTextFile(unsorted);

            // Assert
            Assert.IsTrue(result.FilePath == null);
        }
    }
}
