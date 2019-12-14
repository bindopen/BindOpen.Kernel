using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace BindOpen.Framework.Runtime.Application.Settings
{
    /// <summary>
    /// This class represents a BindOpen host settings.
    /// </summary>
    public class BdoHostSettings : TBdoSettings<BdoHostConfiguration>, IBdoHostSettings
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
        /// The path of the runtime folder.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "runtime.folderPath")]
        public string RuntimeFolderPath { get; internal set; } = (@".\" + BdoDefaultHostPaths.__DefaultRuntimeFolderPath).ToPath();

        /// <summary>
        /// The path of the configuration folder.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "configuration.folderPath")]
        public string ConfigurationFolderPath { get; internal set; } = (@".\" + BdoDefaultHostPaths.__DefaultAppConfigFolderPath).ToPath();

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
        /// The packages folder path of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "packages.folderPath")]
        public string PackagesFolderPath { get; internal set; } = (@".\" + BdoDefaultHostPaths.__DefaultPackagesFolderPath).ToPath();

        /// <summary>
        /// The projects folder path of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "projects.folderPath")]
        public string ProjectsFolderPath { get; internal set; } = (@".\" + BdoDefaultHostPaths.__DefaultProjectsFolderPath).ToPath();

        /// <summary>
        /// The logs file name of this instance.
        /// </summary>
        [XmlIgnore()]
        [DetailProperty(Name = "logs.fileName")]
        public string LogsFileName { get; internal set; } = BdoDefaultHostPaths.__DefaultLogsFileName;

        // Logs ---------------

        /// <summary>
        /// The expiration day number for logs.
        /// </summary>
        /// <remarks>The value -1 means that there is no expiration of logs.</remarks>
        [XmlIgnore()]
        [DetailProperty(Name = "logs.expirationDayNumber")]
        public int LogsExpirationDayNumber { get; internal set; } = -1;

        #endregion

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoHostAppSettings class.
        /// </summary>
        public BdoHostSettings()
            : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoHostAppSettings class.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        public BdoHostSettings(IBdoScope scope, BdoHostConfiguration configuration)
            : base(scope, configuration)
        {
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the runtime folder path.
        /// </summary>
        /// <param name="runtimeFolderPath"></param>
        /// <returns>Returns this instance.</returns>
        public IBdoHostSettings SetRuntimeFolder(string runtimeFolderPath = "")
        {
            RuntimeFolderPath = runtimeFolderPath?.GetEndedString(@"\").ToPath();

            return this;
        }

        /// <summary>
        /// Sets the configuration folder path.
        /// </summary>
        /// <param name="configurationFolderPath"></param>
        /// <returns>Returns this instance.</returns>
        public IBdoHostSettings SetConfigurationFolder(string configurationFolderPath = null)
        {
            ConfigurationFolderPath = configurationFolderPath?.GetEndedString(@"\").ToPath();

            return this;
        }

        /// <summary>
        /// Sets the library folder path.
        /// </summary>
        /// <param name="libraryFolderPath"></param>
        /// <returns>Returns this instance.</returns>
        public IBdoHostSettings SetLibraryFolder(string libraryFolderPath = null)
        {
            LibraryFolderPath = libraryFolderPath?.GetEndedString(@"\").ToPath();

            return this;
        }

        /// <summary>
        /// Sets the logs folder path.
        /// </summary>
        /// <param name="logsFolderPath"></param>
        /// <returns>Returns this instance.</returns>
        public IBdoHostSettings SetLogsFolder(string logsFolderPath = null)
        {
            LogsFolderPath = logsFolderPath?.GetEndedString(@"\").ToPath();

            return this;
        }

        /// <summary>
        /// Sets the packages folder path.
        /// </summary>
        /// <param name="packagesFolderPath"></param>
        /// <returns>Returns this instance.</returns>
        public IBdoHostSettings SetPackagesFolder(string packagesFolderPath = null)
        {
            PackagesFolderPath = packagesFolderPath?.GetEndedString(@"\").ToPath();

            return this;
        }

        /// <summary>
        /// Sets the projects folder path.
        /// </summary>
        /// <param name="projectsFolderPath"></param>
        /// <returns>Returns this instance.</returns>
        public IBdoHostSettings SetProjectsFolder(string projectsFolderPath = null)
        {
            ProjectsFolderPath = projectsFolderPath?.GetEndedString(@"\").ToPath();

            return this;
        }

        /// <summary>
        /// Sets the log file name.
        /// </summary>
        /// <param name="logFileName">The log file name to consider.</param>
        public IBdoHostSettings SetLogsFileName(string logFileName)
        {
            LogsFileName = logFileName;

            return this;
        }

        /// <summary>
        /// Sets the expiration day number for logs.
        /// </summary>
        /// <param name="dayNumber">The expiration day number to consider.</param>
        public IBdoHostSettings SetLogsExpirationDayNumber(int dayNumber)
        {
            if (dayNumber < -1)
            {
                dayNumber = -1;
            }
            LogsExpirationDayNumber = dayNumber;

            return this;
        }

        #endregion
    }
}
