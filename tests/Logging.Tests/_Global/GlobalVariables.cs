using BindOpen.Data.Helpers;
using System.IO;

namespace BindOpen.Logging.Tests;

public static class GlobalVariables
{
    static string _workingFolder = null;

    /// <summary>
    /// The global working folder.
    /// </summary>
    public static string WorkingFolder
    {
        get
        {
            _workingFolder ??= (FileHelper.GetAppRootFolderPath() + @"temp\").ToPath();

            try
            {
                // Create the working folder if it does not already exist
                if (!Directory.Exists(_workingFolder))
                {
                    Directory.CreateDirectory(_workingFolder);
                }
            }
            catch
            {
            }

            return _workingFolder;
        }
    }
}
