using Stein_Samples.Services.FileSystemCompareService.Helper;
using Stein_Samples.Services.FileSystemCompareService.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Stein_Samples.Services.FileSystemCompareService
{
    /// <summary>
    /// Service Implementation with logic for comparing a filesystem
    /// </summary>
    public class FileSystemCompareService : IFileSystemCompareService
    {
        //retrieve the logging instance
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// the algorithm to compare filesystem sets to each other by returning the operations list
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        public IEnumerable<FileSystemCompareOperation> CompareFolder(string path1, string path2)
        {
            var message = string.Empty;                         //error message

            List<FileSystemCompareOperation> operations = new List<FileSystemCompareOperation>();

            //Valdiate paths
            var error = this.CheckDirectoryExists(path1);
            if (!string.IsNullOrEmpty(error))
            {
                Logger.Error(error);     //Log as Error
                operations.Add(new FileSystemCompareOperation(message: error));
            }
            if (path1 != path2) {
                error = this.CheckDirectoryExists(path2);
                if (!string.IsNullOrEmpty(error))
                {
                    Logger.Error(error);     //Log as Error
                    operations.Add(new FileSystemCompareOperation(message: error));
                }
            }

            if (path1 == path2)
            {
                message = "Both Destinations are equal!";
                operations.Add(new FileSystemCompareOperation(message: message));
                Logger.Warn(message);     //Log as Warning
            }

            //check if to continue
            if (operations.Any())
            {
                return operations;
            }

            //identify filesystem sets of both destinations
            IEnumerable<FileSystemItem> files1 = GetFiles(path1);
            IEnumerable<FileSystemItem> files2 = GetFiles(path2);

            //identify common items
            var equalsQuery = (
                from f1 in files1
                join f2 in files2 on f1.RelativePath equals f2.RelativePath
                select f1
            );

            var i = 1;
            //insert items to be deleted by Linq LEFT JOIN
            operations.AddRange(
                (
                    from f in files2
                    join e in equalsQuery on f.RelativePath equals e.RelativePath into result
                    from r in result.DefaultIfEmpty()
                    where r is null
                    orderby f.Type descending, f.RelativePath          //Delete FILE first
                    select new FileSystemCompareOperation(i++, FileOperation.Delete, f)
                )
            );

            //insert items to be created by Linq LEFT JOIN
            operations.AddRange(
                (
                    from f in files1
                    join e in equalsQuery on f.RelativePath equals e.RelativePath into result
                    from r in result.DefaultIfEmpty()
                    where r is null
                    orderby f.Type, f.RelativePath                      //Create DIRECTORY first
                    select new FileSystemCompareOperation(i++, FileOperation.Create, f)
                )
            );

            if (!operations.Any())
            {
                message = "Both Destinations are equal!";
                operations.Add(new FileSystemCompareOperation(message: message));
                Logger.Warn(message);     //Log as Warning
            }

            return operations;
        }

        /// <summary>
        /// generate filesystem list. See https://stackoverflow.com/questions/929276/how-to-recursively-list-all-the-files-in-a-directory-in-c/929418#929418
        /// </summary>
        /// <param name="path"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        public IEnumerable<FileSystemItem> GetFiles(string folder)
        {
            //we process file structure iteration by using a queue
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(folder);

            string path;

            //recursive loop
            while (queue.Count > 0)
            {
                path = queue.Dequeue();

                //Identify Directories
                foreach (string dir in Directory.GetDirectories(path))
                {
                    yield return new FileSystemItem(FileType.Directory, dir.Replace(folder, ""));

                    //search subdirectories
                    queue.Enqueue(dir);
                }

                //Identify Files
                foreach (string file in Directory.GetFiles(path))
                {
                    yield return new FileSystemItem(FileType.File, file.Replace(folder, ""));
                }
            }
        }

        /// <summary>
        /// test if directory exists
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string CheckDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                return string.Format("Invalid or missing Destination Path '{0}'!", path);
            }
            return null;
        }
    }
}
