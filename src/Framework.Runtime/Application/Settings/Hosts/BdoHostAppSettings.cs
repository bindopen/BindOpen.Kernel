using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BindOpen.Framework.Runtime.Application.Settings.Hosts
{
    /// <summary>
    /// This class represents a BindOpen host settings.
    /// </summary>
    public class BdoHostAppSettings : TBdoSettings<BdoHostAppConfiguration>, IBdoHostAppSettings
    {
        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        // Elements ---------------------------

        /// <summary>
        /// Application instance ID of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "applicationInstanceId")]
        public string ApplicationInstanceId { get; set; }

        // Execution ----------------------

        /// <summary>
        /// Environment of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "environment")]
        public string Environment { get; set; }

        /// <summary>
        /// Execution level of this instance.
        /// </summary>
        [XmlIgnore()]
        [DefaultValue(ApplicationExecutionLevel.Production)]
        [DetailProperty(Name = "executionLevel")]
        public ApplicationExecutionLevel ExecutionLevel { get; set; }

        // Platform ----------------------

        /// <summary>
        /// Name of the platform server instance of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "serverInstanceName")]
        public string ServerInstanceName { get; set; }

        /// <summary>
        /// Name of the application instance of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "applicationInstanceName")]
        public string ApplicationInstanceName { get; set; }

        // Folders ---------------

        /// <summary>
        /// The path of the configuration folder.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "configuration.folderPath")]
        public string ConfigurationFolderPath { get; internal set; } = (@".\" + BdoDefaultHostPaths.__DefaultConfigurationFolderPath).ToPath();

        /// <summary>
        /// The logs folder path of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "logs.folderPath")]
        public string LogsFolderPath { get; internal set; } = (@".\" + BdoDefaultHostPaths.__DefaultLogsFolderPath).ToPath();

        /// <summary>
        /// The library folder path of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "library.folderPath")]
        public string LibraryFolderPath { get; internal set; } = (@".\" + BdoDefaultHostPaths.__DefaultLibraryFolderPath).ToPath();

        /// <summary>
        /// The settings folder path of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "packages.folderPath")]
        public string PackagesFolderPath { get; internal set; } = (@".\" + BdoDefaultHostPaths.__DefaultPackagesFolderPath).ToPath();

        /// <summary>
        /// The logs file name of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "logs.fileName")]
        public string LogsFileName { get; internal set; } = BdoDefaultHostPaths.__DefaultLogsFileName;


        #endregion

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoHostAppSettings class.
        /// </summary>
        public BdoHostAppSettings()
            : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoHostAppSettings class.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        public BdoHostAppSettings(IBdoScope scope, BdoHostAppConfiguration configuration)
            : base(scope, configuration)
        {
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the configuration folder path.
        /// </summary>
        /// <param name="configurationFolderPath"></param>
        /// <returns>Returns this instance.</returns>
        public IBdoHostAppSettings SetConfigurationFolder(string configurationFolderPath = null)
        {
            ConfigurationFolderPath = configurationFolderPath?.GetEndedString(@"\").ToPath();

            return this;
        }

        /// <summary>
        /// Sets the library folder path.
        /// </summary>
        /// <param name="libraryFolderPath"></param>
        /// <returns>Returns this instance.</returns>
        public IBdoHostAppSettings SetLibraryFolder(string libraryFolderPath = null)
        {
            LibraryFolderPath = libraryFolderPath?.GetEndedString(@"\").ToPath();

            return this;
        }

        /// <summary>
        /// Sets the logs folder path.
        /// </summary>
        /// <param name="logsFolderPath"></param>
        /// <returns>Returns this instance.</returns>
        public IBdoHostAppSettings SetLogsFolder(string logsFolderPath = null)
        {
            LogsFolderPath = logsFolderPath?.GetEndedString(@"\").ToPath();

            return this;
        }

        /// <summary>
        /// Sets the extensions folder path.
        /// </summary>
        /// <param name="packagesFolderPath"></param>
        /// <returns>Returns this instance.</returns>
        public IBdoHostAppSettings SetPackagesFolder(string packagesFolderPath = null)
        {
            PackagesFolderPath = packagesFolderPath?.GetEndedString(@"\").ToPath();

            return this;
        }

        /// <summary>
        /// Sets the log file name.
        /// </summary>
        /// <param name="logFileName">The log file name to consider.</param>
        public IBdoHostAppSettings SetLogsFileName(string logFileName)
        {
            LogsFileName = logFileName;

            return this;
        }

        #endregion
    }
}
