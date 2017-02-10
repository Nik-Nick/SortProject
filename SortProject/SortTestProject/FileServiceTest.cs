using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using SortProject.Interfaces;
using SortProject.Implementations;

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
        public void TestMethod1()
        {
            //
            // TODO: Add test logic here
            //
        }
    }
}
