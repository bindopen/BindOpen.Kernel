using BindOpen.Framework.System.Diagnostics;
using System;
using System.IO;

namespace BindOpen.Framework.Data.Helpers.Files
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
            catch (Exception ex)
            {
                log?.AddException(ex);
            }

            return isExisting;
        }
    }
}
