using System;
using System.Collections.Generic;
using System.Text;

namespace Stein_Samples.Services.FileSystemCompareService.Helper
{
    /// <summary>
    /// holds the different file types, i.e. Directory or File 
    /// </summary>
    public enum FileType
    {
        Directory,
        File,
        None
    }

    /// <summary>
    /// holds the different file operations
    /// </summary>
    public enum FileOperation
    {
        Create,
        Delete,
        None
    }
}
