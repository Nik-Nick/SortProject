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
        static void Main(string[] args)
        {

            var app = new SortedNameApplication();
            app.Excecute();

            Console.ReadKey();
        }
    }
}
