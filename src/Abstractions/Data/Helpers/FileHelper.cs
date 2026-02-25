using BindOpen.Logging;
using System;
using System.IO;
using System.Reflection;

namespace BindOpen.Data.Helpers;

/// <summary>
/// Provides helper methods for common file system and path operations.
/// </summary>
public static class FileHelper
{
    /// <summary>
    /// Ensures a directory exists at the specified path, logging any errors.
    /// </summary>
    public static bool CreateDirectory(string folderPath, IBdoLog log = null)
    {
        if (string.IsNullOrWhiteSpace(folderPath))
        {
            return false;
        }

        try
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return true;
        }
        catch (Exception ex)
        {
            // Log and return false if creation fails
            log?.AddException(ex);
            return false;
        }
    }

    /// <summary>
    /// Dynamically normalizes path separators for the current OS.
    /// </summary>
    public static string ToPath(this string st)
    {
        if (string.IsNullOrEmpty(st))
            return st;

        // Normalize both forward and backward slashes to the platform directory separator
        return st.Replace('\\', Path.DirectorySeparatorChar)
                 .Replace('/', Path.DirectorySeparatorChar);
    }

    /// <summary>
    /// Converts the specified path according to the environment.
    /// </summary>
    /// <param key="path">The path to consider.</param>
    /// <param key="rootPath">The root path to consider.</param>
    /// <returns></returns>
    public static string GetConcatenatedPath(this string path, string rootPath)
    {
        if (path == null) return null;

        if (path?.StartsWith(@".\") == true || path?.StartsWith(@"./") == true)
        {
            path = (rootPath.EndingWith(@"\") + path.Substring(2)).ToPath();
        }
        else if (path?.StartsWith(@"..\") == true || path?.StartsWith(@"../") == true)
        {
            path = (rootPath.EndingWith(@"\") + path).ToPath();
        }

        return path.ToPath();
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
    /// <param key="folderPath">The folder path to consider.</param>
    /// <param key="expirationDayNumber">The number of expiration days to consider.</param>
    /// <param key="fileFormat">The file format to consider.</param>
    /// <remarks>With expiration day number equaling to -1, no files expires. Equaling to 0, all files except the current one expires.</remarks>
    public static int DeleteExpiredFiles(
        string folderPath,
        int expirationDayNumber,
        string fileFormat)
    {
        // expirationDayNumber == -1 -> no expiration
        if (string.IsNullOrWhiteSpace(folderPath) || expirationDayNumber < 0 || !Directory.Exists(folderPath))
        {
            return 0;
        }

        if (string.IsNullOrEmpty(fileFormat))
        {
            fileFormat = "*.*";
        }

        // Replace known tokens with simple wildcard
        fileFormat = fileFormat.Replace("$(guid)", "*", StringComparison.OrdinalIgnoreCase)
                               .Replace("$(timestamp)", "*", StringComparison.OrdinalIgnoreCase);

        int deleted = 0;
        try
        {
            string[] files = Directory.GetFiles(folderPath, fileFormat);
            var threshold = DateTime.Now.AddDays(-expirationDayNumber);
            foreach (string file in files)
            {
                try
                {
                    var fileInfo = new FileInfo(file);
                    if (!fileInfo.Exists)
                        continue;

                    if (fileInfo.LastAccessTime < threshold)
                    {
                        fileInfo.Delete();
                        deleted++;
                    }
                }
                catch
                {
                    // ignore per-file errors and continue
                }
            }
        }
        catch
        {
            // ignore directory enumeration errors
        }

        return deleted;
    }
}
