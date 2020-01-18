using Stein_Samples.Services.FileSystemCompareService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Console_FileSystemDiffSimulator.Services
{
    /// <summary>
    /// Service class with methods for output
    /// </summary>
    public static class OutputService
    {
        /// <summary>
        /// Prepare custom Console Output
        /// </summary>
        /// <param name="words"></param>
        public static void Output(IEnumerable<FileSystemCompareOperation> items)
        {
            foreach (var item in items.Where(x => !string.IsNullOrEmpty(x.Message)))
            {
                //First display any information/warning/error messages
                Console.WriteLine(item.Message);
            }

            foreach (var item in items.Where(x => string.IsNullOrEmpty(x.Message)))
            {
                //display formatted content
                Console.WriteLine(string.Format("{0,-8}{1,-24}{2, -24}", item.Step.ToString() + ".", item.OperationText, item.Item.RelativePath));
            }
        }
    }
}
