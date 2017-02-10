using System.Collections.Generic;
using System.IO;

namespace SortProject
{
    public class TextFile
    {
        public string FileName { get; set; }

        public string FilePath { get; set; }

        public List<string> DataRows { get; set; }

        public TextFile()
        {

        }

        public TextFile(string fileName, string filePath, List<string> dataRows)
        {
            FileName = fileName;
            FilePath = filePath;
            DataRows = dataRows;
        }

        public string GetSavePath()
        {
            return Path.Combine(FilePath, FileName + ".txt");
        }


    }
}
