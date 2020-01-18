
namespace WPF_Samples.Helper
{
    public static class Utils
    {
        /// <summary>
        /// adds the ending slash to the path if missing
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string CheckPathEnding(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }

            if (path[path.Length - 1] != '\\')
            {
                path = string.Concat(path, '\\');
            }

            return path;
        }
    }
}
