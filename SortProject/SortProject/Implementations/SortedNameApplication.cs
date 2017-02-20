using Autofac;
using SortProject.Implementations;
using SortProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortProject
{
    public class SortedNameApplication : ISortedNameApplication
    {

        private IOutput _output { get; set; }

        private INameService _nameService { get; set; }

        private IFileService _fileService { get; set; }

        public SortedNameApplication(IOutput output, INameService nameService, IFileService fileService)
        {
            _output = output;
            _nameService = nameService;
            _fileService = fileService;
        }


        public void Excecute()
        {
            var textFile = _fileService.GetTextFileFromUser();

            _output.WriteLine("sort - names " + textFile.GetSavePath());

            var sortedTextFile = _fileService.CreateSortedTextFile(textFile);

            //Filter invalid name
            if(textFile.DataRows.Count != sortedTextFile.DataRows.Count)
            {
                _output.WriteLine("Incorrect " + (textFile.DataRows.Count - sortedTextFile.DataRows.Count) + " name/s");
            }

            sortedTextFile.FileName = _fileService.CreateNonExistingFileName(sortedTextFile.FileName, sortedTextFile.FilePath, ".txt");

            //Create new file
            _fileService.WriteTextFile(sortedTextFile.DataRows, sortedTextFile.GetSavePath());
            _fileService.OutputDataFromFile(sortedTextFile);
            _output.Write("Finished: created " + sortedTextFile.FileName + ".txt");

        }
    }
}
