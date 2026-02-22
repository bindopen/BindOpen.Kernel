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
        var exists = false;
        try
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            exists = true;
        }
        catch (IOException ex)
        {
            log?.AddException(ex);
        }
        return exists;
    }

    /// <summary>
    /// Dynamically normalizes path separators for the current OS.
    /// </summary>
    public static string ToPath(this string st)
    {
        return st?.Replace('\\', Path.DirectorySeparatorChar);
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
