using Autofac;
using SortProject.Implementations;
using SortProject.Interfaces;
using System;

namespace SortProject
{
    /// <summary>
    /// Create sorted by name text file from unsorted name file
    /// Author: Nikom Dupuskull
    /// Date: 10/2/2017
    /// </summary>
    public class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Output>().As<IOutput>();
            builder.RegisterType<NameService>().As<INameService>();
            builder.RegisterType<FileService>().As<IFileService>();
            builder.RegisterType<SortedNameApplication>().As<ISortedNameApplication>();

            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                var app = new SortedNameApplication(scope.Resolve<IOutput>(), scope.Resolve<INameService>(), scope.Resolve<IFileService>());
                app.Excecute();
            }

            Console.ReadKey();
        }
    }
}
