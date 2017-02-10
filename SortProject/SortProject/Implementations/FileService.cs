using SortProject.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace SortProject.Implementations
{
    public class FileService: IFileService
    {
        private IOutput _output;
        private INameService _nameService;

        public FileService(IOutput output, INameService nameService)
        {
            _output = output;
            _nameService = nameService;
        }

        /// <summary>
        /// Read file from user input
        /// </summary>
        /// <returns></returns>
        public TextFile GetTextFileFromUser()
        {
            bool isFound = false;
            var textFile = new TextFile();

            while (!isFound)
            {
                _output.WriteLine("Please insert file name");
                _output.Write(@"C:\");
                var file = @"C:\" + Console.ReadLine();

                try
                {
                    if (Path.GetExtension(file) == ".txt")
                    {

                        textFile.DataRows = new List<string>(File.ReadAllLines(file));

                        if (textFile.DataRows.Count > 0)
                        {
                            textFile.FileName = Path.GetFileNameWithoutExtension(file);
                            textFile.FilePath = Path.GetDirectoryName(file);
                            isFound = true;
                        }
                        else
                        {
                            _output.WriteLine("File is empty");
                        }
                    }
                    else
                    {
                        _output.WriteLine("Incorrect file type");
                    }
                }
                catch
                {
                    _output.WriteLine("Invalid file");
                }
            }
            return textFile;
        }

        /// <summary>
        /// Create sorted file from unsorted file
        /// </summary>
        /// <param name="textFile">unsorted file</param>
        /// <returns>sorted file</returns>
        public TextFile CreateSortedTextFile(TextFile textFile)
        {
            if(textFile.DataRows == null)
            {
                return textFile;
            }
            var nameList = _nameService.CreateNameList(textFile.DataRows);
            nameList = _nameService.SortedNameList(nameList);
            var dataRows = new List<string>();
            foreach(var name in nameList)
            {
                dataRows.Add(name.GetLastNameFirstName());
            }

            return new TextFile {
                FileName = textFile.FileName + "-sorted",
                FilePath = textFile.FilePath,
                DataRows = dataRows
            };
        }

        /// <summary>
        /// Create text file to givent file path
        /// </summary>
        /// <param name="dataRows">text lines</param>
        /// <param name="savePath">file path + file name</param>
        public void WriteTextFile(List<string> dataRows, string savePath)
        {
            var writer = File.CreateText(savePath);
            foreach(var row in dataRows)
            {
                writer.WriteLine(row);
            }
            writer.Close();
        }

        /// <summary>
        /// Create non exsiting file name
        /// </summary>
        /// <param name="fileName">original name</param>
        /// <param name="filePath">file path</param>
        /// <param name="fileType">file type</param>
        /// <returns>new name</returns>
        public string CreateNonExistingFileName(string fileName, string filePath, string fileType)
        {
            var newName = fileName;
            var copy = 1;
            while(File.Exists(Path.Combine(filePath, newName + fileType)))
            {
                newName = fileName + "(" + copy + ")";
                copy++;
            }

            return newName;
        }

        /// <summary>
        /// Write data to console
        /// </summary>
        /// <param name="textFile">text file</param>
        public void OutputDataFromFile(TextFile textFile)
        {
            foreach(var line in textFile.DataRows)
            {
                _output.WriteLine(line);
            }
        }
    }
}
