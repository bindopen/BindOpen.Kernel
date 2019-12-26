using BindOpen.Framework.Core.Application.Exceptions;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Stores;
using BindOpen.Framework.Core.Extensions.References;
using BindOpen.Framework.Core.Extensions.Runtime.Stores;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Runtime.Application.Hosts;
using BindOpen.Framework.Runtime.Application.Modules;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Runtime.Application.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Framework.Runtime.Application.Options
{
    /// <summary>
    /// This class represents a host options.
    /// </summary>
    public class TBdoHostOptions<S> : DataItem, ITBdoHostOptions<S>
        where S : class, IBdoAppSettings, new()
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
        /// The output kinds of the default loggers.
        /// </summary>
        protected HashSet<DatasourceKind> _defaultLoggerOutputKinds = new HashSet<DatasourceKind>();

        /// <summary>
        /// 
        /// </summary>
        protected IExtensionLoadOptions _extensionLoadOptions = null;

        /// <summary>
        /// The root folder path.
        /// </summary>
        protected List<(Predicate<ITBdoHostOptions<S>> Predicate, string RootFolderPath)> _rootFolderPathDefinitions = null;

        // Paths ----------------------

        /// <summary>
        /// 
        /// </summary>
        protected string _rootFolderPath = null;

        /// <summary>
        /// 
        /// </summary>
        protected string _hostConfigFilePath = (@".\" + BdoDefaultHostPaths.__DefaultHostConfigFileName).ToPath();

        /// <summary>
        /// Indicates whether the host configuration file is required.
        /// </summary>
        /// <remarks>If it does not exist then an exception is thrown.</remarks>
        protected bool _isHostConfigFileRequired = false;

        // Depots ----------------------

        /// <summary>
        /// 
        /// </summary>
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
        public string Environment => HostSettings?.Environment ?? _environment;

        // Paths ----------------------

        /// <summary>
        /// The root folder path.
        /// </summary>
        public string RootFolderPath => _rootFolderPath;

        /// <summary>
        /// The root folder path.
        /// </summary>
        public List<(Predicate<ITBdoHostOptions<S>> Predicate, string RootFolderPath)> RootFolderPathDefinitions => _rootFolderPathDefinitions;

        /// <summary>
        /// The hot configuration file path.
        /// </summary>
        public string HostConfigFilePath => _hostConfigFilePath;

        /// <summary>
        /// Indicates whether the host configuration file must exist.
        /// </summary>
        /// <remarks>If it does not exist then an exception is thrown.</remarks>
        public bool IsHostConfigFileRequired => _isHostConfigFileRequired;

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
        /// The output kinds of the default logger.
        /// </summary>
        /// <remarks>If there is none then we do not have any default logger.</remarks>
        public HashSet<DatasourceKind> DefaultLoggerOutputKinds => _defaultLoggerOutputKinds;

        // Settings ----------------------

        /// <summary>
        /// The host settings of this instance.
        /// </summary>
        public IBdoHostSettings HostSettings { get; set; }

        /// <summary>
        /// The application settings of this instance.
        /// </summary>
        public IBdoAppSettings AppSettings => Settings;

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

        /// <summary>
        /// The data store of this instance.
        /// </summary>
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
        public T GetSettings<T>() where T : class, IBdoAppSettings
            => AppSettings as T;

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

            if (specificationAreas == null || specificationAreas.Contains(BdoHostPathKind.RootFolder.ToString()))
            {
                if (string.IsNullOrEmpty(_rootFolderPath))
                {
                    _rootFolderPath = BdoDefaultHostPaths.__DefaultRootFolderPath;
                }
                else
                {
                    _rootFolderPath = _rootFolderPath.GetConcatenatedPath(BdoDefaultHostPaths.__DefaultRootFolderPath).GetEndedString(@"\").ToPath();
                }
            }

            if (specificationAreas == null)
            {
                _hostConfigFilePath = _hostConfigFilePath.GetConcatenatedPath(_rootFolderPath).ToPath();

                HostSettings?.UpdateRuntimeInfo(null, null, log);
                HostSettings?.WithRuntimeFolder(HostSettings?.RuntimeFolderPath.GetConcatenatedPath(_rootFolderPath).GetEndedString(@"\").ToPath());
                HostSettings?.WithAppConfigFile(HostSettings?.AppConfigurationFolderPath.GetConcatenatedPath(HostSettings?.RuntimeFolderPath).GetEndedString(@"\").ToPath(), HostSettings?.IsAppConfigFileRequired ?? false);
                HostSettings?.WithLibraryFolder(HostSettings?.LibraryFolderPath.GetConcatenatedPath(HostSettings?.RuntimeFolderPath).GetEndedString(@"\").ToPath());
                HostSettings?.WithLogsFolder(HostSettings?.LogsFolderPath.GetConcatenatedPath(HostSettings?.RuntimeFolderPath).GetEndedString(@"\").ToPath());
                HostSettings?.WithPackagesFolder(HostSettings?.PackagesFolderPath.GetConcatenatedPath(HostSettings?.RuntimeFolderPath).GetEndedString(@"\").ToPath());
                HostSettings?.WithProjectsFolder(HostSettings?.ProjectsFolderPath.GetConcatenatedPath(HostSettings?.RuntimeFolderPath).GetEndedString(@"\").ToPath());

                ExtensionLoadOptions?.WithLibraryFolderPath(HostSettings?.LibraryFolderPath);
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
        /// Set the root folder.
        /// </summary>
        /// <param name="predicate">The condition that must be satisfied.</param>
        /// <param name="rootFolderPath">The root folder path.</param>
        /// <returns>Returns the host option.</returns>
        public ITBdoHostOptions<S> SetRootFolder(Predicate<ITBdoHostOptions<S>> predicate, string rootFolderPath)
        {
            _rootFolderPathDefinitions = _rootFolderPathDefinitions ?? new List<(Predicate<ITBdoHostOptions<S>> Predicate, string RootFolderPath)>();
            _rootFolderPathDefinitions.Add((predicate, rootFolderPath));

            return this;
        }

        /// <summary>
        /// Sets the specified root folder path.
        /// </summary>
        /// <param name="path">The path to consider.</param>
        /// <returns>Returns this instance.</returns>
        public ITBdoHostOptions<S> SetRootFolder(string path)
        {
            _rootFolderPath = path?.GetEndedString(@"\").ToPath();

            return this;
        }

        /// <summary>
        /// Set the specified host configuration file path.
        /// </summary>
        /// <param name="path">The settings file path.</param>
        /// <param name="isRequired">Indicates whether the host configuration file is required.</param>
        /// <returns>Returns the host option.</returns>
        public ITBdoHostOptions<S> SetHostConfigFile(string path, bool isRequired = false)
        {
            _hostConfigFilePath = path?.ToPath();
            return SetHostConfigFile(isRequired);
        }

        /// <summary>
        /// Set the specified host configuration file path.
        /// </summary>
        /// <param name="path">The settings file path.</param>
        /// <param name="isRequired">Indicates whether the host configuration file is required.</param>
        /// <returns>Returns the host option.</returns>
        public ITBdoHostOptions<S> SetHostConfigFile(bool isRequired)
        {
            _isHostConfigFileRequired = isRequired;

            return this;
        }

        // Extensions -------------------------------------------

        /// <summary>
        /// Adds the extensions.
        /// </summary>
        /// <param name="loadOptionsAction">The action for loading options.</param>
        /// <param name="action">The action for adding extensions.</param>
        /// <returns>Returns the host option.</returns>
        public ITBdoHostOptions<S> AddExtensions(Action<IExtensionLoadOptions> loadOptionsAction, Action<List<IBdoExtensionReference>> action)
        {
            loadOptionsAction?.Invoke(ExtensionLoadOptions);
            return AddExtensions(action);
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
        public ITBdoHostOptions<S> SetHostSettings(Action<IBdoHostSettings> action)
        {
            HostSettings = HostSettings ?? new BdoHostSettings();
            action?.Invoke(HostSettings);

            return this;
        }

        /// <summary>
        /// Defines the specified default settings.
        /// </summary>
        /// <param name="specificationSet">The set of data element specifcations to consider.</param>
        /// <returns>Returns this instance.</returns>
        public virtual ITBdoHostOptions<S> DefineAppSettings(IDataElementSpecSet specificationSet)
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
            _defaultLoggerOutputKinds.Add(DatasourceKind.Repository);

            HostSettings = HostSettings ?? new BdoHostSettings();
            HostSettings?.WithLogsFileName(logFileName);

            return this;
        }

        /// <summary>
        /// Adds the default console logger.
        /// </summary>
        /// <returns>Returns this instance.</returns>
        public ITBdoHostOptions<S> AddDefaultConsoleLogger()
        {
            _defaultLoggerOutputKinds.Add(DatasourceKind.Console);

            return this;
        }

        // Trigger actions -------------------------------------------

        /// <summary>  
        /// The action that is executed when the start of this instance succedes.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public ITBdoHostOptions<S> ExecuteOnStartSuccess(Action<ITBdoHost<S>> action)
        {
            Action_OnStartSuccess = new Action<ITBdoService<S>>(p => action?.Invoke(p as ITBdoHost<S>));

            return this;
        }

        /// <summary>
        /// The action that is executed when the start of this instance fails.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public ITBdoHostOptions<S> ExecuteOnStartFailure(Action<ITBdoHost<S>> action)
        {
            Action_OnStartFailure = new Action<ITBdoService<S>>(p => action?.Invoke(p as ITBdoHost<S>));

            return this;
        }

        /// <summary>
        /// The action that is executed when this instance is successfully completed.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public ITBdoHostOptions<S> ExecuteOnExecutionSuccess(Action<ITBdoHost<S>> action)
        {
            Action_OnExecutionSucess = new Action<ITBdoService<S>>(p => action?.Invoke(p as ITBdoHost<S>));

            return this;
        }

        /// <summary>
        /// The action that is executed when this instance execution fails.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public ITBdoHostOptions<S> ExecuteOnExecutionFailure(Action<ITBdoHost<S>> action)
        {
            Action_OnExecutionFailure = new Action<ITBdoService<S>>(p => action?.Invoke(p as ITBdoHost<S>));

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

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);

            if (isDisposing)
            {
                _applicationModule?.Dispose();
                _extensionLoadOptions?.Dispose();
                _dataStore?.Dispose();
            }
        }

        #endregion
    }
}