using NLog;
using Stein_Samples.Services.FileSystemCompareService;
using Stein_Samples.Services.FileSystemCompareService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Console.Samples.Services
{
    /// <summary>
    /// Service class with formatted output
    /// </summary>
    public class FileSystemCompareOutputService
    {
        /// <summary>
        /// Prepare custom Console Output
        /// </summary>
        /// <param name="words"></param>
        public void Output(IEnumerable<FileSystemCompareOperation> items)
        {
            if (items is null) { return; }

            foreach (var item in items.Where(x => !string.IsNullOrEmpty(x.Message)))
            {
                // display any information/warning/error messages
                System.Console.WriteLine(item.Message);
            }

            foreach (var item in items.Where(x => string.IsNullOrEmpty(x.Message)))
            {
                // display formatted content
                System.Console.WriteLine(string.Format("{0,-8}{1,-24}{2, -24}", item.Step.ToString() + ".", item.OperationText, item.Item.RelativePath));
            }
        }
    }

    /// <summary>
    /// reads two Folder Inputs, compares all files including subfolders and simulates all operations needed to make the second destination equal to the second destination
    /// </summary>
    public class FileSystemCompareConsoleService : IConsoleService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public static readonly string Title = "Filesystem Diff Simulator";

        private readonly IFileSystemCompareService _fileSystemCompareService;
        private readonly FileSystemCompareOutputService _outputService;

        public FileSystemCompareConsoleService(
            IFileSystemCompareService fileSystemCompareService,
            FileSystemCompareOutputService outputService)
        {
            _fileSystemCompareService = fileSystemCompareService;
            _outputService = outputService;
        }

        public void Start()
        {
            System.Console.WriteLine($"### {Title} ###");

            // iterate logic until user stops execution
            do
            {
                System.Console.Write("Enter first Destination Path: ");
                string _Path1 = System.Console.ReadLine();

                System.Console.Write("Enter second Destination Path: ");
                string _Path2 = System.Console.ReadLine();

                Logger.Debug(string.Concat("Input 1: ", _Path1));         // Log Input1
                Logger.Debug(string.Concat("Input 2: ", _Path2));         // Log Input2

                IEnumerable<FileSystemCompareOperation> result = null;
                try
                {
                    // retrieve result from the associated Service
                    result = _fileSystemCompareService.CompareFolder(_Path1, _Path2);
                }
                catch (UnauthorizedAccessException ex)
                {
                    Logger.Error(ex.Message);
                    System.Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.Fatal(ex.Message);
                    throw;
                }

                //provide console output of the result items
                _outputService.Output(result);

                // get the user input for every iteration, allowing to exit at will
                System.Console.WriteLine("Continue [y|n]?");
                var input = System.Console.ReadKey(true);
                if (input.Key == ConsoleKey.N)
                {
                    break;
                }
            }
            while (true);
        }
    }
}
