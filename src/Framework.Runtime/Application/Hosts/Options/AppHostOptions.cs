using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Runtime.Application.Modules;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Runtime.Application.Hosts.Options
{
    /// <summary>
    /// This class represents an application host configuration.
    /// </summary>
    public class AppHostOptions : IAppHostOptions
    {
        // ------------------------------------------
        // CONSTANTS
        // ------------------------------------------

        #region Constants

        /// <summary>
        /// The default settings file name
        /// </summary>
        public static readonly string _DEFAULTSETTINGSFILENAME = "appsettings.xml";

        /// <summary>
        /// Maximum number of minutes : authentication 
        /// </summary>
        public static readonly int _AUTHENTICATIONTIMEOUT = 30;

        #endregion

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private AppModule _applicationModule = null;

        private Logger[] _loggers = null;

        private AppExtensionConfiguration _appExtensionConfiguration = null;

        private bool _isDefaultLoggerUsed = false;

        // Paths ----------------------

        private string _appFolderPath = null;

        private string _settingsFilePath = null;

        private string _runtimeFolderPath = null;

        private string _libraryFolderPath = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The settings.
        /// </summary>
        public AppSettings Settings { get; set; }

        /// <summary>
        /// The application module.
        /// </summary>
        public AppModule ApplicationModule => _applicationModule;

        // Paths

        /// <summary>
        /// The application folder path.
        /// </summary>
        public string AppFolderPath => _appFolderPath;

        /// <summary>
        /// The settings file path.
        /// </summary>
        public string SettingsFilePath => _settingsFilePath;

        /// <summary>
        /// The runtime folder path.
        /// </summary>
        public string RuntimeFolderPath => _runtimeFolderPath;

        /// <summary>
        /// The library folder path.
        /// </summary>
        public string LibraryFolderPath => _libraryFolderPath;

        /// <summary>
        /// The runtime folder path.
        /// </summary>
        public Logger[] Loggers { get; }

        /// <summary>
        /// Indicates whether the default logger is used.
        /// </summary>
        public bool IsDefaultLoggerUsed => _isDefaultLoggerUsed;

        /// <summary>
        /// The extension configuration.
        /// </summary>
        public AppExtensionConfiguration ExtensionConfiguration => _appExtensionConfiguration;

        // Settings

        /// <summary>
        /// The set of settings specifications of this instance.
        /// </summary>
        public DataElementSpecSet SettingsSpecificationSet { get; set; } = new DataElementSpecSet();

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the AppHostOptions class.
        /// </summary>
        public AppHostOptions()
        {
        }

        #endregion

        // ------------------------------------------
        // SETTERS
        // ------------------------------------------

        #region Usage

        /// <summary>
        /// Sets the specified application folder path.
        /// </summary>
        /// <param name="appFolderPath"></param>
        /// <returns>Returns this instance.</returns>
        public IAppHostOptions SetAppFolder(string appFolderPath)
        {
            this._appFolderPath = appFolderPath.GetEndedString(@"\").ToPath();

            return this;
        }

        /// <summary>
        /// Sets the runtime folder path.
        /// </summary>
        /// <param name="runtimeFolderPath"></param>
        /// <returns>Returns this instance.</returns>
        public IAppHostOptions SetRuntimeFolder(string runtimeFolderPath)
        {
            this._runtimeFolderPath = runtimeFolderPath.GetEndedString(@"\").ToPath();
            return this;
        }

        /// <summary>
        /// Sets the library folder path.
        /// </summary>
        /// <param name="libraryFolderPath">The libarry folder path</param>
        /// <returns>Returns this instance.</returns>
        public IAppHostOptions SetLibraryFolder(string libraryFolderPath)
        {
            this._libraryFolderPath = libraryFolderPath.GetEndedString(@"\").ToPath();
            return this;
        }

        /// <summary>
        /// Sets the specified module.
        /// </summary>
        /// <param name="module"></param>
        /// <returns>Returns this instance.</returns>
        public IAppHostOptions SetModule(AppModule module)
        {
            this._applicationModule = module;
            return this;
        }

        /// <summary>
        /// Sets the specified extension settings.
        /// </summary>
        /// <param name="extensionConfiguration"></param>
        /// <returns>Returns this instance.</returns>
        public IAppHostOptions SetExtensions(AppExtensionConfiguration extensionConfiguration)
        {
            if (extensionConfiguration != null)
            {
                (this._appExtensionConfiguration ?? (this._appExtensionConfiguration = new AppExtensionConfiguration())).Merge(extensionConfiguration);

                if (this._appExtensionConfiguration.DefaultSourceKinds == null)
                    this._appExtensionConfiguration.DefaultSourceKinds = new List<DataSourceKind>() { DataSourceKind.Memory, DataSourceKind.Repository };
            }

            return this;
        }

        /// <summary>
        /// Sets the specified settings file.
        /// </summary>
        /// <typeparam name="T">The settings class to consider.</typeparam>
        /// <param name="settingsFilePath">The path of the settings file to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IAppHostOptions SetSettingsFile<T>(string settingsFilePath = null) where T : AppSettings, new()
        {
            this.DefineSettings<T>();

            if (settingsFilePath!=null)
            {
                if (settingsFilePath.StartsWith(@"\") || settingsFilePath.EndsWith(@"\") || settingsFilePath.EndsWith(@"\.."))
                {
                    settingsFilePath = settingsFilePath.GetEndedString(@"\") + _DEFAULTSETTINGSFILENAME;
                }

                this._settingsFilePath = settingsFilePath.ToPath();
            }

            return this;
        }

        /// <summary>
        /// Defines the specified settings.
        /// </summary>
        /// <param name="specificationSet">The set of data element specifcations to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IAppHostOptions DefineSettings(DataElementSpecSet specificationSet)
        {
            this.SettingsSpecificationSet = specificationSet ?? new DataElementSpecSet();

            return this;
        }

        /// <summary>
        /// Defines the specified settings.
        /// </summary>
        /// <typeparam name="T">The settings class to consider.</typeparam>
        /// <param name="specificationSet">The set of data element specifcations to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IAppHostOptions DefineSettings<T>(DataElementSpecSet specificationSet = null) where T : AppSettings, new()
        {
            this.Settings = new T();
            this.DefineSettings(specificationSet);

            return this;
        }

        /// <summary>
        /// Sets the specified settings specification.
        /// </summary>
        /// <param name="loggers">The loggers to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IAppHostOptions SetLoggers(params Logger[] loggers)
        {
            this._loggers = loggers;

            return this;
        }

        /// <summary>
        /// Adds the default logger.
        /// </summary>
        /// <returns>Returns this instance.</returns>
        public IAppHostOptions AddDefaultLogger()
        {
            _isDefaultLoggerUsed = true;

            return this;
        }

        #endregion
    }
}