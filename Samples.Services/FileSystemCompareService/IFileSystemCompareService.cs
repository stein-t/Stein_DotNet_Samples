using Samples.Services.FileSystemCompareService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.Services.FileSystemCompareService
{
    /// <summary>
    /// Contract for the FileSystemCompareService
    /// </summary>
    public interface IFileSystemCompareService
    {
        /// <summary>
        /// the algorithm to compare filesystem sets to each other by returning the operations list
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        IEnumerable<FileSystemCompareOperation> CompareFolder(string path1, string path2);

        /// <summary>
        /// test if directory exists
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string CheckDirectoryExists(string path);
    }
}
