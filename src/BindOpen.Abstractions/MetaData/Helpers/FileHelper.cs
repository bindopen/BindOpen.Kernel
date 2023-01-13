using BindOpen.Logging;
using System;
using System.IO;
using System.Reflection;

namespace BindOpen.MetaData
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
                path = (rootPath.EndingWith(@"\") + path.Substring(2)).ToPath();
            }
            else if ((path?.StartsWith(@"..\") == true) || (path?.StartsWith(@"../") == true))
            {
                path = (rootPath.EndingWith(@"\") + path).ToPath();
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

            return rootFolderPath?.EndingWith(@"\").ToPath();
        }


        /// <summary>
        /// Delete the files that are older than the specified day number.
        /// </summary>
        /// <param name="folderPath">The folder path to consider.</param>
        /// <param name="expirationDayNumber">The number of expiration days to consider.</param>
        /// <param name="fileFormat">The file format to consider.</param>
        /// <remarks>With expiration day number equaling to -1, no files expires. Equaling to 0, all files except the current one expires.</remarks>
        public static int DeleteExpiredFiles(
            string folderPath,
            int expirationDayNumber,
            string fileFormat)
        {
            int i = 0;
            if (expirationDayNumber > -1 && Directory.Exists(folderPath))
            {
                if (fileFormat == null)
                {
                    fileFormat = "*.*";
                }
                fileFormat = fileFormat.Replace("$(guid)", StringHelper.__Star, StringComparison.OrdinalIgnoreCase)
                    .Replace("$(timestamp)", StringHelper.__Star, StringComparison.OrdinalIgnoreCase);

                string[] files = Directory.GetFiles(folderPath, fileFormat);

                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    if (fileInfo.LastAccessTime < DateTime.Now.AddDays(-expirationDayNumber))
                    {
                        try
                        {
                            fileInfo.Delete();
                            i++;
                        }
                        catch
                        {
                        }
                    }
                }
            }

            return i;
        }
    }
}
