using Samples.Services.FileSystemCompareService.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.Services.FileSystemCompareService.Models
{
    /// <summary>
    /// Model holding File (or Directory) path information
    /// </summary>
    public class FileSystemItem
    {
        public FileType Type { get; set; }
        public string RelativePath { get; set; }

        public FileSystemItem() { }
        public FileSystemItem(FileType type, string relativePath)
        {
            Type = type;
            RelativePath = relativePath;
        }
    }
}
