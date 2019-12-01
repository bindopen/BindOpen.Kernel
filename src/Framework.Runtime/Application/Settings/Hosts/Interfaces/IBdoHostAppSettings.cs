using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.System;

namespace BindOpen.Framework.Runtime.Application.Settings.Hosts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoHostAppSettings : ITBdoSettings<BdoHostAppConfiguration>
    {
        /// <summary>
        /// 
        /// </summary>
        new BdoHostAppConfiguration Configuration { get; }

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
        string PackagesFolderPath { get; }

        /// <summary>
        /// Set the configuration folder.
        /// </summary>
        /// <param name="configFolderPath">The configuration folder path.</param>
        /// <returns>Returns the host option.</returns>
        IBdoHostAppSettings SetConfigurationFolder(string configFolderPath);

        /// <summary>
        /// Set the library folder.
        /// </summary>
        /// <param name="logsFolderPath">The logs folder path.</param>
        /// <returns>Returns the host option.</returns>
        IBdoHostAppSettings SetLogsFolder(string logsFolderPath);

        /// <summary>
        /// Set the library folder.
        /// </summary>
        /// <param name="libraryFolderPath">The library folder path.</param>
        /// <returns>Returns the host option.</returns>
        IBdoHostAppSettings SetLibraryFolder(string libraryFolderPath);

        /// <summary>
        /// Set the packages folder.
        /// </summary>
        /// <param name="packagesFolderPath">The packages folder path.</param>
        /// <returns>Returns the host option.</returns>
        IBdoHostAppSettings SetPackagesFolder(string packagesFolderPath);
    }
}