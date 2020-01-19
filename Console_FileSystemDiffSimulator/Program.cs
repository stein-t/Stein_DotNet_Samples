using Console_FileSystemDiffSimulator.Services;
using Stein_Samples.Services.FileSystemCompareService;
using Stein_Samples.Services.FileSystemCompareService.Models;
using System;
using System.Collections.Generic;

namespace Console_FileSystemDiffSimulator
{
    class Program
    {
        /// <summary>
        /// reads 2 Folder Inputs, compares all files including subfolders and simulates all operations needed to make the second destination equal to the second destination
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //retrieve the logging instance
            NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

            //retrieve the associated service
            IFileSystemCompareService _FileSystemCompareService = new FileSystemCompareService();

            Logger.Info("Application started");
            Console.WriteLine("#### Filesystem Diff Simulator - Willkommen ####");

            IEnumerable<FileSystemCompareOperation> _Items;     //holds the result from the service

            // iterate logic until user stops execution
            do
            {
                _Items = null;

                Console.Write("Enter first Destination Path: ");
                string _Path1 = Console.ReadLine();

                Console.Write("Enter second Destination Path: ");
                string _Path2 = Console.ReadLine();

                Logger.Debug(string.Concat("Input 1: ", _Path1));         //Log Input1
                Logger.Debug(string.Concat("Input 2: ", _Path2));         //Log Input2

                try
                {
                    //retrieve result from the associated Service
                    _Items = _FileSystemCompareService.CompareFolder(_Path1, _Path2);
                }
                catch (UnauthorizedAccessException ex)
                {
                    Logger.Error(ex.Message);       //Log Exception as Error
                    Console.WriteLine(ex.Message);  //here we can continue execution                          
                }
                catch (Exception ex)
                {
                    Logger.Fatal(ex.Message);       //Log Exception
                    throw;
                }

                if (_Items != null)
                {
                    //provide console output of the items
                    OutputService.Output(_Items);
                }

                // get the user input for every iteration, allowing to exit at will
                Console.Write("Continue [y|n]?");
                var text = Console.ReadLine();
                if (text.Equals("n"))
                {
                    // exit the method.
                    break;
                }
            }
            while (true);
        }
    }
}
