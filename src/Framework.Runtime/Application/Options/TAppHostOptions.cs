using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.Application.Modules;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Runtime.Application.Options
{
    /// <summary>
    /// This class represents an application host options.
    /// </summary>
    public class TAppHostOptions<Q> : DataItem, ITAppHostOptions<Q>
        where Q : AppConfiguration, new()
    {
        // ------------------------------------------
        // CONSTANTS
        // ------------------------------------------

        #region Constants

        /// <summary>
        /// The default settings file name
        /// </summary>
        public static readonly string __DefaultSettingsFileName = "appconfig.xml";

        /// <summary>
        /// Maximum number of minutes : authentication 
        /// </summary>
        public static readonly int _AUTHENTICATIONTIMEOUT = 30;

        #endregion

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private IAppModule _applicationModule = null;

        private ILogger[] _loggers = null;

        private IAppExtensionConfiguration _appExtensionConfiguration = null;

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
        public ITAppSettings<Q> Settings { get; set; }

        /// <summary>
        /// The base settings.
        /// </summary>
        public IBaseAppSettings BaseSettings => Settings as IBaseAppSettings;

        /// <summary>
        /// The application module.
        /// </summary>
        public IAppModule ApplicationModule => _applicationModule;

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
        public ILogger[] Loggers { get; }

        /// <summary>
        /// Indicates whether the default logger is used.
        /// </summary>
        public bool IsDefaultLoggerUsed => _isDefaultLoggerUsed;

        /// <summary>
        /// The extension configuration.
        /// </summary>
        public IAppExtensionConfiguration ExtensionConfiguration => _appExtensionConfiguration;

        // Settings

        /// <summary>
        /// The set of settings specifications of this instance.
        /// </summary>
        public IDataElementSpecSet SettingsSpecificationSet { get; set; } = new DataElementSpecSet();

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the AppHostOptions class.
        /// </summary>
        public TAppHostOptions()
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
        public ITAppHostOptions<Q> SetAppFolder(string appFolderPath)
        {
            this._appFolderPath = appFolderPath.GetEndedString(@"\").ToPath();

            return this;
        }

        /// <summary>
        /// Sets the runtime folder path.
        /// </summary>
        /// <param name="runtimeFolderPath"></param>
        /// <returns>Returns this instance.</returns>
        public ITAppHostOptions<Q> SetRuntimeFolder(string runtimeFolderPath)
        {
            this._runtimeFolderPath = runtimeFolderPath.GetEndedString(@"\").ToPath();
            return this;
        }

        /// <summary>
        /// Sets the library folder path.
        /// </summary>
        /// <param name="libraryFolderPath">The libarry folder path</param>
        /// <returns>Returns this instance.</returns>
        public ITAppHostOptions<Q> SetLibraryFolder(string libraryFolderPath)
        {
            this._libraryFolderPath = libraryFolderPath.GetEndedString(@"\").ToPath();
            return this;
        }

        /// <summary>
        /// Sets the specified module.
        /// </summary>
        /// <param name="module"></param>
        /// <returns>Returns this instance.</returns>
        public ITAppHostOptions<Q> SetModule(IAppModule module)
        {
            this._applicationModule = module;
            return this;
        }

        /// <summary>
        /// Sets the specified extension settings.
        /// </summary>
        /// <param name="extensionConfiguration"></param>
        /// <returns>Returns this instance.</returns>
        public ITAppHostOptions<Q> SetExtensions(IAppExtensionConfiguration extensionConfiguration)
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
        public ITAppHostOptions<Q> SetSettingsFile<T>(string settingsFilePath = null)
            where T : ITAppSettings<Q>, new()
        {
            this.DefineSettings<T>();

            if (settingsFilePath!=null)
            {
                if (settingsFilePath.StartsWith(@"\") || settingsFilePath.EndsWith(@"\") || settingsFilePath.EndsWith(@"\.."))
                {
                    settingsFilePath = settingsFilePath.GetEndedString(@"\") + __DefaultSettingsFileName;
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
        public ITAppHostOptions<Q> DefineSettings(IDataElementSpecSet specificationSet)
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
        public ITAppHostOptions<Q> DefineSettings<T>(IDataElementSpecSet specificationSet = null)
            where T : ITAppSettings<Q>, new()
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
        public ITAppHostOptions<Q> SetLoggers(params ILogger[] loggers)
        {
            this._loggers = loggers;

            return this;
        }

        /// <summary>
        /// Adds the default logger.
        /// </summary>
        /// <returns>Returns this instance.</returns>
        public ITAppHostOptions<Q> AddDefaultLogger()
        {
            _isDefaultLoggerUsed = true;

            return this;
        }

        #endregion
    }
}