using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.System;

namespace BindOpen.Framework.Runtime.Application.Settings.Hosts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoHostSettings : ITBdoSettings<BdoHostConfiguration>
    {
        /// <summary>
        /// 
        /// </summary>
        new BdoHostConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        string ApplicationInstanceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ApplicationInstanceName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Environment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ApplicationExecutionLevel ExecutionLevel { get; set; }

        // Paths ----------------------

        /// <summary>
        /// 
        /// </summary>
        string RuntimeFolderPath { get; }

        /// <summary>
        /// 
        /// </summary>
        string ConfigurationFolderPath { get; }

        /// <summary>
        /// 
        /// </summary>
        string LibraryFolderPath { get; }

        /// <summary>
        /// 
        /// </summary>
        string LogsFolderPath { get; }

        /// <summary>
        /// 
        /// </summary>
        string LogsFileName { get; }

        /// <summary>
        /// 
        /// </summary>
        string PackagesFolderPath { get; }

        /// <summary>
        /// 
        /// </summary>
        string ProjectsFolderPath { get; }

        /// <summary>
        /// 
        /// </summary>
        int LogsExpirationDayNumber { get; }

        /// <summary>
        /// Set the runtime folder.
        /// </summary>
        /// <param name="runtimeFolderPath">The runtime folder path.</param>
        /// <returns>Returns the host option.</returns>
        IBdoHostSettings SetRuntimeFolder(string runtimeFolderPath);

        /// <summary>
        /// Set the configuration folder.
        /// </summary>
        /// <param name="configFolderPath">The configuration folder path.</param>
        /// <returns>Returns the host option.</returns>
        IBdoHostSettings SetConfigurationFolder(string configFolderPath);

        /// <summary>
        /// Set the library folder.
        /// </summary>
        /// <param name="logsFolderPath">The logs folder path.</param>
        /// <returns>Returns the host option.</returns>
        IBdoHostSettings SetLogsFolder(string logsFolderPath);

        /// <summary>
        /// Set the library folder.
        /// </summary>
        /// <param name="libraryFolderPath">The library folder path.</param>
        /// <returns>Returns the host option.</returns>
        IBdoHostSettings SetLibraryFolder(string libraryFolderPath);

        /// <summary>
        /// Set the packages folder.
        /// </summary>
        /// <param name="packagesFolderPath">The packages folder path.</param>
        /// <returns>Returns the host option.</returns>
        IBdoHostSettings SetPackagesFolder(string packagesFolderPath);

        /// <summary>
        /// Set the projects folder.
        /// </summary>
        /// <param name="projectsFolderPath">The projects folder path.</param>
        /// <returns>Returns the host option.</returns>
        IBdoHostSettings SetProjectsFolder(string projectsFolderPath = null);

        /// <summary>
        /// Sets the log file name.
        /// </summary>
        /// <param name="logFileName">The log file name to consider.</param>
        IBdoHostSettings SetLogsFileName(string logFileName);

        /// <summary>
        /// Sets the expiration day number for logs.
        /// </summary>
        /// <param name="dayNumber">The expiration day number to consider.</param>
        IBdoHostSettings SetLogsExpirationDayNumber(int dayNumber);

    }
}