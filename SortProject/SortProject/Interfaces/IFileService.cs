using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortProject.Interfaces
{
    public interface IFileService
    {
        TextFile GetTextFileFromUser();
        TextFile CreateSortedTextFile(TextFile textFile);
        void WriteTextFile(List<string> dataRows, string savePath);
        string CreateNonExistingFileName(string fileName, string filePath, string fileType);
        void OutputDataFromFile(TextFile textFile);
    }
}
