using Stein_Samples.Services.FileSystemCompareService.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stein_Samples.Services.FileSystemCompareService.Models
{
    /// <summary>
    /// Model that holds result items for the DIFF operations list
    /// </summary>
    public class FileSystemCompareOperation
    {
        /// <summary>
        /// Helper to display Error/Warnings/Information messages in the target result control
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The order of the operation
        /// </summary>
        public int? Step { get; set; }

        /// <summary>
        /// The type of operation
        /// </summary>
        public FileOperation Operation { get; set; }

        /// <summary>
        /// The operation text to represent in the UI
        /// </summary>
        public string OperationText { get; set; }

        /// <summary>
        /// Reference to the Path item
        /// </summary>
        public FileSystemItem Item { get; set; }

        public FileSystemCompareOperation(int? step = null, FileOperation operation = FileOperation.None, FileSystemItem item = null, string message = null)
        {
            Step = step;
            Operation = operation;
            Item = item ?? new FileSystemItem(FileType.None, null);     //initialize empty object
            Message = message;

            OperationText = operation == FileOperation.None ? string.Empty : string.Concat(operation.ToString().ToUpper(), " ", item.Type.ToString().ToUpper());
        }
    }
}
