using BindOpen.Application.Configuration;
using BindOpen.System;

namespace BindOpen.Application.Settings
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
        string AppConfigurationFolderPath { get; }

        /// <summary>
        /// Indicates whether the application configuration file must exist.
        /// </summary>
        /// <remarks>If it does not exist then an exception is thrown.</remarks>
        bool? IsAppConfigFileRequired { get; }

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
        IBdoHostSettings WithRuntimeFolder(string runtimeFolderPath);

        /// <summary>
        /// Set the application configuration file.
        /// </summary>
        /// <param name="configFolderPath">The application configuration folder path.</param>
        /// <param name="isRequired">Indicates whether the application configuration file is required.</param>
        /// <returns>Returns the host option.</returns>
        IBdoHostSettings WithAppConfigFile(string configFolderPath, bool? isRequired = false);

        /// <summary>
        /// Set the application configuration file.
        /// </summary>
        /// <param name="isRequired">Indicates whether the application configuration file is required.</param>
        /// <returns>Returns the host option.</returns>
        IBdoHostSettings WithAppConfigFileRequired(bool? isRequired);

        /// <summary>
        /// Set the library folder.
        /// </summary>
        /// <param name="logsFolderPath">The logs folder path.</param>
        /// <returns>Returns the host option.</returns>
        IBdoHostSettings WithLogsFolder(string logsFolderPath);

        /// <summary>
        /// Set the library folder.
        /// </summary>
        /// <param name="libraryFolderPath">The library folder path.</param>
        /// <returns>Returns the host option.</returns>
        IBdoHostSettings WithLibraryFolder(string libraryFolderPath);

        /// <summary>
        /// Set the packages folder.
        /// </summary>
        /// <param name="packagesFolderPath">The packages folder path.</param>
        /// <returns>Returns the host option.</returns>
        IBdoHostSettings WithPackagesFolder(string packagesFolderPath);

        /// <summary>
        /// Set the projects folder.
        /// </summary>
        /// <param name="projectsFolderPath">The projects folder path.</param>
        /// <returns>Returns the host option.</returns>
        IBdoHostSettings WithProjectsFolder(string projectsFolderPath = null);

        /// <summary>
        /// Sets the log file name.
        /// </summary>
        /// <param name="logFileName">The log file name to consider.</param>
        IBdoHostSettings WithLogsFileName(string logFileName = null);

        /// <summary>
        /// Sets the expiration day number for logs.
        /// </summary>
        /// <param name="dayNumber">The expiration day number to consider.</param>
        IBdoHostSettings WithLogsExpirationDayNumber(int dayNumber);
    }
}