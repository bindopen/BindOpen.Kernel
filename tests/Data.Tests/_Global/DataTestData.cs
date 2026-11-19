using BindOpen.Data.Helpers;

namespace BindOpen;

/// <summary>
/// Provides global static data and path management for the BindOpen test suite.
/// </summary>
public static class DataTestData
{
    static string _workingFolder;

    /// <summary>
    /// Gets the global working folder path used for test outputs and temporary files.
    /// </summary>
    /// <value>The absolute path to the temporary working directory.</value>
    /// <remarks>
    /// This property uses lazy initialization. It automatically resolves the application root 
    /// folder and appends a 'temp' directory, ensuring the path is properly formatted 
    /// using the <see cref="StringHelper.ToPath"/> extension.
    /// </remarks>
    public static string WorkingFolder
    {
        get
        {
            if (_workingFolder == null)
            {
                // Combines the application root with the temp folder and normalizes the path
                _workingFolder = (FileHelper.GetAppRootFolderPath() + @"temp\").ToPath();
            }

            return _workingFolder;
        }
    }
}