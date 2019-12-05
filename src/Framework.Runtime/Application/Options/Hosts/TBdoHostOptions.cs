using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Depots;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Datasources;
using BindOpen.Framework.Core.Data.Stores;
using BindOpen.Framework.Core.Extensions.References;
using BindOpen.Framework.Core.Extensions.Runtime.Stores;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Runtime.Application.Exceptions;
using BindOpen.Framework.Runtime.Application.Hosts;
using BindOpen.Framework.Runtime.Application.Modules;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Runtime.Application.Settings.Hosts;
using BindOpen.Framework.Runtime.System.Diagnostics.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Framework.Runtime.Application.Options.Hosts
{
    /// <summary>
    /// This class represents a host options.
    /// </summary>
    public class TBdoHostOptions<S> : DataItem, ITBdoHostOptions<S>
        where S : class, IBdoHostSettings, new()
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        /// <summary>
        /// 
        /// </summary>
        protected IAppModule _applicationModule = null;

        /// <summary>
        /// 
        /// </summary>
        protected string _environment = null;

        /// <summary>
        /// 
        /// </summary>
        protected IList<IBdoLogger> _loggers = null;

        /// <summary>
        /// 
        /// </summary>
        protected IExtensionLoadOptions _extensionLoadOptions = null;

        /// <summary>
        /// 
        /// </summary>
        protected bool _isDefaultLoggerUsed = false;

        // Paths ----------------------

        /// <summary>
        /// 
        /// </summary>
        protected string _appFolderPath = null;

        /// <summary>
        /// 
        /// </summary>
        protected string _appSettingsFilePath = (@".\" + BdoDefaultHostPaths.__DefaultAppSettingsFileName).ToPath();

        // Depots ----------------------

        protected IBdoDataStore _dataStore = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The application module.
        /// </summary>
        public IAppModule ApplicationModule => _applicationModule;

        /// <summary>
        /// The settings.
        /// </summary>
        public string Environment => AppSettings?.Environment ?? _environment;

        // Paths ----------------------

        /// <summary>
        /// The application folder path.
        /// </summary>
        public string AppFolderPath => _appFolderPath;

        /// <summary>
        /// The settings file path.
        /// </summary>
        public string AppSettingsFilePath => _appSettingsFilePath;

        // Extensions ----------------------

        /// <summary>
        /// The extension loading options.
        /// </summary>
        public List<IBdoExtensionReference> ExtensionReferences { get; set; } = new List<IBdoExtensionReference>();

        /// <summary>
        /// The extension loading options.
        /// </summary>
        public IExtensionLoadOptions ExtensionLoadOptions => _extensionLoadOptions;

        // Loggers ----------------------

        /// <summary>
        /// The loggers of this instance.
        /// </summary>
        public IList<IBdoLogger> Loggers { get; }

        /// <summary>
        /// Indicates whether the default logger is used.
        /// </summary>
        public bool IsDefaultFileLoggerUsed => _isDefaultLoggerUsed;

        // Settings ----------------------

        /// <summary>
        /// The application settings of this instance.
        /// </summary>
        public IBdoHostAppSettings AppSettings { get; set; }

        /// <summary>
        /// The settings of this instance as host settings.
        /// </summary>
        public IBdoHostSettings HostSettings => Settings;

        /// <summary>
        /// The settings of this instance.
        /// </summary>
        public S Settings { get; set; }

        /// <summary>
        /// The set of settings specifications of this instance.
        /// </summary>
        public IDataElementSpecSet SettingsSpecificationSet { get; set; } = new DataElementSpecSet();

        // Trigger actions ----------------------

        /// <summary>
        /// The action that the start of this instance completes.
        /// </summary>
        public Action<ITBdoService<S>> Action_OnStartSuccess { get; set; }

        /// <summary>
        /// The action that the start of this instance fails.
        /// </summary>
        public Action<ITBdoService<S>> Action_OnStartFailure { get; set; }

        /// <summary>
        /// The action that this instance completes.
        /// </summary>
        public Action<ITBdoService<S>> Action_OnExecutionSucess { get; set; }

        /// <summary>
        /// The action that is executed when the instance fails.
        /// </summary>
        public Action<ITBdoService<S>> Action_OnExecutionFailure { get; set; }

        // Depot initialization actions ----------------------

        public IBdoDataStore DataStore => _dataStore;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoHostOptions class.
        /// </summary>
        public TBdoHostOptions() : base()
        {
            _extensionLoadOptions = new ExtensionLoadOptions()
                .WithLibraryFolderPath((@".\" + BdoDefaultHostPaths.__DefaultLibraryFolderPath).ToPath())
                .WithSourceKinds(DatasourceKind.Memory, DatasourceKind.Repository);
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Get the settings as the specified host settings class.
        /// </summary>
        /// <typeparam name="T">The host settings class to consider.</typeparam>
        public T GetSettings<T>() where T : class, IBdoHostSettings
            => HostSettings as T;

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <returns>The log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override IBdoLog Update<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateModes[] updateModes = null)
        {
            IBdoLog log = new BdoLog();

            if (specificationAreas == null || specificationAreas.Contains(BdoHostPathKind.AppFolder.ToString()))
            {
                if (string.IsNullOrEmpty(AppFolderPath))
                {
                    _appFolderPath = BdoDefaultHostPaths.__DefaultAppFolderPath;
                }
                else
                {
                    _appFolderPath = _appFolderPath.GetConcatenatedPath(BdoDefaultHostPaths.__DefaultAppFolderPath).GetEndedString(@"\").ToPath();
                }
            }

            if (specificationAreas == null)
            {
                _appSettingsFilePath = _appSettingsFilePath.GetConcatenatedPath(_appFolderPath).ToPath();

                AppSettings?.UpdateRuntimeInfo(null, null, log);
                AppSettings?.SetRuntimeFolder(AppSettings?.RuntimeFolderPath.GetConcatenatedPath(_appFolderPath).GetEndedString(@"\").ToPath());
                AppSettings?.SetConfigurationFolder(AppSettings?.ConfigurationFolderPath.GetConcatenatedPath(AppSettings?.RuntimeFolderPath).GetEndedString(@"\").ToPath());
                AppSettings?.SetLibraryFolder(AppSettings?.LibraryFolderPath.GetConcatenatedPath(AppSettings?.RuntimeFolderPath).GetEndedString(@"\").ToPath());
                AppSettings?.SetLogsFolder(AppSettings?.LogsFolderPath.GetConcatenatedPath(AppSettings?.RuntimeFolderPath).GetEndedString(@"\").ToPath());
                AppSettings?.SetPackagesFolder(AppSettings?.PackagesFolderPath.GetConcatenatedPath(AppSettings?.RuntimeFolderPath).GetEndedString(@"\").ToPath());

                ExtensionLoadOptions?.WithLibraryFolderPath(AppSettings?.LibraryFolderPath);
            }

            return log;
        }

        // Modules -------------------------------------------

        /// <summary>
        /// Sets the specified module.
        /// </summary>
        /// <param name="module"></param>
        /// <returns>Returns this instance.</returns>
        public ITBdoHostOptions<S> SetModule(IAppModule module)
        {
            _applicationModule = module;
            return this;
        }

        /// <summary>
        /// Sets the specified module.
        /// </summary>
        /// <param name="moduleName">The name of the module.</param>
        /// <returns>Returns this instance.</returns>
        public ITBdoHostOptions<S> SetModule(string moduleName)
        {
            _applicationModule = new AppModule(moduleName);
            return this;
        }

        /// <summary>
        /// Sets the specified environment.
        /// </summary>
        /// <param name="environment">The environment to consider.</param>
        /// <returns>Returns this instance.</returns>
        public ITBdoHostOptions<S> SetEnvironment(string environment)
        {
            _environment = environment;
            return this;
        }

        // Paths -------------------------------------------

        /// <summary>
        /// Sets the specified application folder path.
        /// </summary>
        /// <param name="appFolderPath"></param>
        /// <returns>Returns this instance.</returns>
        public ITBdoHostOptions<S> SetAppFolder(string appFolderPath)
        {
            _appFolderPath = appFolderPath?.GetEndedString(@"\").ToPath();

            return this;
        }
        /// <summary>
        /// Set the settings file.
        /// </summary>
        /// <param name="settingsFilePath">The settings file path.</param>
        /// <returns>Returns the host option.</returns>
        public ITBdoHostOptions<S> SetSettingsFile(string settingsFilePath)
        {
            _appSettingsFilePath = settingsFilePath?.ToPath();

            return this;
        }

        // Extensions -------------------------------------------

        /// <summary>
        /// Configures the extension load of this instance.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public ITBdoHostOptions<S> ConfigureExtensionLoad(Action<IExtensionLoadOptions> action)
        {
            action?.Invoke(ExtensionLoadOptions);
            return this;
        }

        /// <summary>
        /// Sets the specified extensions.
        /// </summary>
        /// <param name="action">The action to invoke on extensions.</param>
        /// <returns>Returns this instance.</returns>
        public ITBdoHostOptions<S> AddExtensions(Action<List<IBdoExtensionReference>> action)
        {
            action?.Invoke(ExtensionReferences);
            return this;
        }

        // Settings -------------------------------------------

        /// <summary>
        /// Defines the application settings.
        /// </summary>
        /// <param name="action">The action to consider on the settings.</param>
        /// <returns>Returns the host option.</returns>
        public ITBdoHostOptions<S> SetAppSettings(Action<IBdoHostAppSettings> action)
        {
            AppSettings = AppSettings ?? new BdoHostAppSettings();
            action?.Invoke(AppSettings);

            return this;
        }

        /// <summary>
        /// Defines the specified default settings.
        /// </summary>
        /// <param name="specificationSet">The set of data element specifcations to consider.</param>
        /// <returns>Returns this instance.</returns>
        public virtual ITBdoHostOptions<S> DefineSettings(IDataElementSpecSet specificationSet)
        {
            SettingsSpecificationSet = specificationSet ?? new DataElementSpecSet();

            return this;
        }

        // Logs -------------------------------------------

        /// <summary>
        /// Adds the specified settings specification.
        /// </summary>
        /// <param name="loggers">The loggers to consider.</param>
        /// <returns>Returns this instance.</returns>
        public ITBdoHostOptions<S> AddLoggers(params IBdoLogger[] loggers)
        {
            if (_loggers == null) _loggers = new List<IBdoLogger>();
            loggers?.ToList().ForEach(p => _loggers.Add(p));

            return this;
        }

        /// <summary>
        /// Adds the default file logger.
        /// </summary>
        /// <param name="logFileName">The log file name to consider.</param>
        /// <returns>Returns this instance.</returns>
        public ITBdoHostOptions<S> AddDefaultFileLogger(string logFileName = null)
        {
            _isDefaultLoggerUsed = true;

            AppSettings = AppSettings ?? new BdoHostAppSettings();
            AppSettings?.SetLogsFileName(logFileName);

            return this;
        }

        /// <summary>
        /// Adds the default console logger.
        /// </summary>
        /// <returns>Returns this instance.</returns>
        public ITBdoHostOptions<S> AddDefaultConsoleLogger()
        {
            AddLoggers(BdoLoggerFactory.Create<BdoSnapLogger>(null, BdoLoggerMode.Auto, DatasourceKind.Console));

            return this;
        }

        // Trigger actions -------------------------------------------

        /// <summary>  
        /// The action that is executed when the start of this instance succedes.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public ITBdoHostOptions<S> OnStartSuccess(Action<ITBdoService<S>> action)
        {
            Action_OnStartSuccess = action;

            return this;
        }

        /// <summary>
        /// The action that is executed when the start of this instance fails.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public ITBdoHostOptions<S> OnStartFailure(Action<ITBdoService<S>> action)
        {
            Action_OnStartFailure = action;

            return this;
        }

        /// <summary>
        /// The action that is executed when this instance is successfully completed.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public ITBdoHostOptions<S> OnExecutionSuccess(Action<ITBdoService<S>> action)
        {
            Action_OnExecutionSucess = action;

            return this;
        }

        /// <summary>
        /// The action that is executed when this instance execution fails.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public ITBdoHostOptions<S> OnExecutionFailure(Action<ITBdoService<S>> action)
        {
            Action_OnExecutionFailure = action;

            return this;
        }

        /// <summary>
        /// Throws an exception when start fails.
        /// </summary>
        public ITBdoHostOptions<S> ThrowExceptionOnStartFailure()
        {
            Action_OnStartFailure = (_ => throw new BdoHostLoadException("BindOpen host failed while loading"));

            return this;
        }

        /// <summary>
        /// Throws an exception when start fails.
        /// </summary>
        public ITBdoHostOptions<S> ThrowExceptionOnExecutionFailure()
        {
            Action_OnExecutionFailure = (_ => throw new BdoHostLoadException("BindOpen host failed while loading"));

            return this;
        }

        // Depots -------------------------------------------

        /// <summary>
        /// Adds the data store executing the specified action.
        /// </summary>
        /// <param name="action">The action to execute on the created data store.</param>
        public ITBdoHostOptions<S> AddDataStore(Action<IBdoDataStore> action = null)
        {
            _dataStore = _dataStore ?? new BdoDataStore();
            action?.Invoke(_dataStore);

            return this;
        }

        #endregion
    }
}