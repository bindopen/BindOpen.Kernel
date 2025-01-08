using BindOpen.Data.Helpers;

namespace BindOpen;

public static class DataTestData
{
    static string _workingFolder;

    /// <summary>
    /// The global working folder.
    /// </summary>
    public static string WorkingFolder
    {
        get
        {
            if (_workingFolder == null)
            {
                _workingFolder = (FileHelper.GetAppRootFolderPath() + @"temp\").ToPath();
            }

            return _workingFolder;
        }
    }
}
