using BindOpen.Data.Helpers.Strings;
using BindOpen.System.Diagnostics;
using System;
using System.IO;
using System.Reflection;

namespace BindOpen.Data.Helpers.Files
{
    /// <summary>
    /// This class represents a helper for files.
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Creates the specified file directory if it does not exist.
        /// </summary>
        /// <param name="folderPath">The path of the file directory to consider.</param>
        /// <param name="log">The log to append.</param>
        /// <returns>Returns True whether the directory exists henceforth. False otherwise.</returns>
        public static bool CreateDirectory(string folderPath, IBdoLog log = null)
        {
            var isExisting = false;
            try
            {
                // we create the folder if it does not exist
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                isExisting = true;
            }
            catch (IOException ex)
            {
                log?.AddException(ex);
            }

            return isExisting;
        }

        /// <summary>
        /// Converts the specified path according to the environment.
        /// </summary>
        /// <param name="st">The string to consider.</param>
        /// <returns></returns>
        public static string ToPath(this string st)
        {
            return st?.Replace('\\', Path.DirectorySeparatorChar);
        }

        /// <summary>
        /// Converts the specified path according to the environment.
        /// </summary>
        /// <param name="path">The path to consider.</param>
        /// <param name="rootPath">The root path to consider.</param>
        /// <returns></returns>
        public static string GetConcatenatedPath(this string path, string rootPath)
        {
            if (path == null) return null;

            if ((path?.StartsWith(@".\") == true) || (path?.StartsWith(@"./") == true))
            {
                path = (rootPath.GetEndedString(@"\") + path.Substring(2)).ToPath();
            }
            else if ((path?.StartsWith(@"..\") == true) || (path?.StartsWith(@"../") == true))
            {
                path = (rootPath.GetEndedString(@"\") + path).ToPath();
            }

            return path?.Replace('\\', Path.DirectorySeparatorChar);
        }

        /// <summary>
        /// Gets the path of the application root folder.
        /// </summary>
        /// <returns>Returns the path.</returns>
        public static string GetAppRootFolderPath()
        {
            string currentFolderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string rootFolderPath = null;

            while (string.IsNullOrEmpty(rootFolderPath) && !string.IsNullOrEmpty(currentFolderPath))
            {
                currentFolderPath = Path.GetDirectoryName(currentFolderPath);
                if (string.Equals(Path.GetFileName(currentFolderPath), "bin", StringComparison.OrdinalIgnoreCase))
                {
                    rootFolderPath = currentFolderPath = Path.GetDirectoryName(currentFolderPath);
                }
            }
            if (string.IsNullOrEmpty(rootFolderPath))
            {
                rootFolderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }

            return rootFolderPath?.GetEndedString(@"\").ToPath();
        }

    }
}
