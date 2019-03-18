using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Schema;
using BindOpen.Framework.Core.Application.Datasources;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.Core.System.Assemblies;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Core.System.Processing;
using BindOpen.Framework.Runtime.Application.Modules;
using BindOpen.Framework.Runtime.Application.Security;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// This class represents an application manager.
    /// </summary>
    public class AppService : ScopedService, IAppService
    {
        // ------------------------------------------
        // CONSTANTS
        // ------------------------------------------

        #region Constants

        public static readonly string _DefaultSettingsFileName = "appsettings.xml";

        #endregion

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        // General ----------------------

        private readonly String _id = "";

        private AppModule _applicationModule = null;
        private AppExtensionConfiguration _appExtensionConfiguration = null;

        //private LoggerMode _LoggerMode = LoggerMode.Manual;

        // Paths ----------------------

        /// <summary>
        /// The application folder path of this instance.
        /// </summary>
        private String _appFolderPath = null;

        /// <summary>
        /// The application configuration file path of this instance.
        /// </summary>
        private String _settingsFilePath = null;

        /// <summary>
        /// The roaming application folder path of this instance.
        /// </summary>
        private String _runtimeFolderPath = null;

        // Tracking ----------------------

        private readonly Log _log;

        /// <summary>
        /// Indicates whether this instance is loaded.
        /// </summary>
        /// <remarks>The value can be assigned.</remarks>
        protected Boolean _isLoadCompleted = false;

        // Extensions ----------------------

        /// <summary>
        /// This delegate is called when the application is successfully initialized.
        /// </summary>
        /// <param name="sender">The application manager.</param>
        public delegate void OnLoadCompletedEventHandler(object sender);

        /// <summary>
        /// This event is triggered when the application is successfully initialized.
        /// </summary>
        public event OnLoadCompletedEventHandler OnLoadCompleted;

        #endregion

        // ------------------------------------------
        // CONSTANTS
        // ------------------------------------------

        #region Constants

        /// <summary>
        /// Maximum number of minutes : authentication 
        /// </summary>
        public const int _AUTHENTICATIONTIMEOUT = 30;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        // Settings ----------------------

        /// <summary>
        /// The application module of this instance.
        /// </summary>
        public AppModule ApplicationModule
        {
            get
            {
                return this._applicationModule;
            }
        }

        /// <summary>
        /// The settings of this instance.
        /// </summary>
        public AppSettings Settings { get; set; } = null;

        /// <summary>
        /// The set of user settings of this instance.
        /// </summary>
        public DataElementSet UserSettingsSet { get; set; } = new DataElementSet();

        /// <summary>
        /// The set of settings specifications of this instance.
        /// </summary>
        public DataElementSpecificationSet SettingsSpecificationSet { get; set; } = new DataElementSpecificationSet();

        // Execution ----------------------

        /// <summary>
        /// The current execution state of this instance.
        /// </summary>
        public ProcessExecutionState CurrentExecutionState
        {
            get { return ((this._log != null) && (this._log.Task != null) ? this._log.Execution.State : ProcessExecutionState.None); }
            set { if ((_log != null) && (this._log.Task != null)) this._log.Execution.State = value; }
        }

        /// <summary>
        /// The current execution status of this instance.
        /// </summary>
        public ProcessExecutionStatus CurrentExecutionStatus
        {
            get { return ((this._log != null) && (this._log.Task != null) ? this._log.Execution.Status : ProcessExecutionStatus.None); }
            set { if ((_log != null) && (this._log.Task != null)) this._log.Execution.Status = value; }
        }

        /// <summary>
        /// Execution status of this instance.
        /// </summary>
        public TimeSpan AckPeriod { get; set; } = new TimeSpan();

        // Loading information ----------------------

        /// <summary>
        /// Application domain pool of this instance.
        /// </summary>
        public AppDomainPool AppDomainPool { get; set; } = null;

        // Tracking ----------------------

        /// <summary>
        /// Log of this instance.
        /// </summary>
        public Log Log
        {
            get { return this._log; }
        }

        /// <summary>
        /// Indicates whether the platform information is loaded.
        /// </summary>
        public Boolean IsLoadCompleted
        {
            get { return this._isLoadCompleted; }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ApplicationManager class.
        /// </summary>
        public AppService()
        {
            // we determine the file path
            this._id = IdentifiedDataItem.NewGuid();

            // we initiate the log of this instance
            this._log = new Log(_ => false)
            {
                Id = this._id
            };

            this.UseAppFolder(Directory.GetCurrentDirectory());

            this.UseExtensions(
                new AppExtensionConfiguration(
                    new AppExtensionFilter("BindOpen.Framework.Runtime"),
                    new AppExtensionFilter("BindOpen.Framework.Standard")));

            this.UseSettings<AppSettings>();

            // we instantiate the loaded extension handler and the application script interperter
            this.AppDomainPool = new AppDomainPool();
        }

        #endregion

        // ------------------------------------------
        // USAGE
        // ------------------------------------------

        #region Usage

        /// <summary>
        /// Uses the specified application folder path.
        /// </summary>
        /// <param name="appFolderPath"></param>
        /// <returns>Returns this instance.</returns>
        public IAppService UseAppFolder(String appFolderPath)
        {
            this._appFolderPath = appFolderPath.GetEndedString(@"\").ToPath();

            return this;
        }

        /// <summary>
        /// Uses the specified application settings file path.
        /// </summary>
        /// <param name="appSettingsFilePath"></param>
        /// <returns>Returns this instance.</returns>
        public IAppService UseSettingsFile(String appSettingsFilePath)
        {
            if (appSettingsFilePath?.StartsWith(@"\") == true || appSettingsFilePath?.EndsWith(@"\") == true || appSettingsFilePath?.EndsWith(@"\..") == true)
            {
                appSettingsFilePath = appSettingsFilePath.GetEndedString(@"\") + AppService._DefaultSettingsFileName;
            }

            this._settingsFilePath = appSettingsFilePath.ToPath();

            return this;
        }

        /// <summary>
        /// Uses the specified runtime folder path.
        /// </summary>
        /// <param name="runtimeFolderPath"></param>
        /// <returns>Returns this instance.</returns>
        public IAppService UseRuntimeFolder(String runtimeFolderPath)
        {
            this._runtimeFolderPath = runtimeFolderPath.GetEndedString(@"\").ToPath();
            return this;
        }

        /// <summary>
        /// Uses the specified module.
        /// </summary>
        /// <param name="module"></param>
        /// <returns>Returns this instance.</returns>
        public IAppService UseModule(AppModule module)
        {
            this._applicationModule = module;
            return this;
        }

        /// <summary>
        /// Uses the specified extension configuration.
        /// </summary>
        /// <param name="extensionConfiguration"></param>
        /// <returns>Returns this instance.</returns>
        public IAppService UseExtensions(AppExtensionConfiguration extensionConfiguration)
        {
            if (extensionConfiguration != null)
            {

                (this._appExtensionConfiguration ?? (this._appExtensionConfiguration = new AppExtensionConfiguration())).Merge(extensionConfiguration);

                if (this._appExtensionConfiguration.DefaultFolderPath == null)
                    this._appExtensionConfiguration.DefaultFolderPath = this._appFolderPath;
                else if (this._appExtensionConfiguration.DefaultSourceKinds == null)
                    this._appExtensionConfiguration.DefaultSourceKinds = new List<DataSourceKind>() { DataSourceKind.Memory, DataSourceKind.Repository };
            }

            return this;
        }

        /// <summary>
        /// Uses the specified configuration specification.
        /// </summary>
        /// <typeparam name="T">The application configuration class to consider.</typeparam>
        /// <param name="configFilePath">The path of the configuration file to consider.</param>
        /// <param name="specificationSet">The set of data element specifcations to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IAppService UseSettings<T>(
            string configFilePath = null,
            DataElementSpecificationSet specificationSet = null)
            where T : AppSettings, new()
        {
            this.Settings = new T();
            this.SettingsSpecificationSet = specificationSet ?? new DataElementSpecificationSet();

            return this;
        }

        /// <summary>
        /// Uses the specified configuration specification.
        /// </summary>
        /// <typeparam name="T">The application configuration class to consider.</typeparam>
        /// <param name="specificationSet">The set of data element specifcations to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IAppService UseSettings<T>(
            DataElementSpecificationSet specificationSet)
            where T : AppSettings, new()
        {
            return this.UseSettings<T>(null, specificationSet);
        }

        /// <summary>
        /// Uses the specified configuration specification.
        /// </summary>
        /// <param name="specificationSet"></param>
        /// <returns>Returns this instance.</returns>
        public IAppService UseLoggers(params Logger[] loggers)
        {
            this._log?.AddLoggers(loggers);

            return this;
        }

        /// <summary>
        /// Uses the default logger.
        /// </summary>
        /// <returns>Returns this instance.</returns>
        public IAppService UseDefaultLogger()
        {
            this._log?.AddLoggers(LoggerFactory.Create<SnapLogger>(
                    Logger.__DefaultName, LoggerMode.Auto, DataSourceKind.Repository, false, null,
                    this.GetPath(ApplicationPathKind.DefaultLogFolder), "log_" + this._id + ".txt"));

            return this;
        }

        private string GetDefaultSettingsFilePath()
        {
            // by default, settings file is {{runtime folder}}\settings\appsettings.xml
            string defaultSettingsFilePath = string.IsNullOrEmpty(this._runtimeFolderPath) ? null : this._runtimeFolderPath + @"settings\".ToPath() + AppService._DefaultSettingsFileName;

            if (!File.Exists(defaultSettingsFilePath))
            {
                // by default, settings file is {{runtime folder}}\appsettings.xml
                defaultSettingsFilePath = string.IsNullOrEmpty(this._runtimeFolderPath) ? null : this._runtimeFolderPath + AppService._DefaultSettingsFileName;
                if (!File.Exists(defaultSettingsFilePath))
                {
                    // then {{application folder}}\app_data\appsettings.xml
                    defaultSettingsFilePath = string.IsNullOrEmpty(this._appFolderPath) ? null : this._appFolderPath + @"app_data\".ToPath() + AppService._DefaultSettingsFileName;
                    if (!File.Exists(defaultSettingsFilePath))
                    {
                        // then {{application folder}}\appsettings.xml
                        defaultSettingsFilePath = this._appFolderPath + AppService._DefaultSettingsFileName;
                    }
                }
            }

            return defaultSettingsFilePath;
        }

        #endregion

        // ------------------------------------------
        // PROCESSING
        // ------------------------------------------

        #region Processing

        /// <summary>
        /// Starts the application.
        /// </summary>
        /// <returns>Returns true if this instance is started.</returns>
        public virtual IAppService Start(Log log = null)
        {
            log = log ?? new Log();

            if (this.CurrentExecutionState != ProcessExecutionState.Pending)
            {
                // we start the application instance
                log.AddMessage("Starting application instance...");
                log.Start();

                // we initialize this instance
                log.AddMessage("Initializing application...");
                log.Append(this.Initialize());

                if (!this.IsLoadCompleted)
                    this.CurrentExecutionStatus = ProcessExecutionStatus.Stopped_Error;
                else
                    log.AddMessage("Application instance started");

                log.Sanitize();

                this._log.Append(log);
            }

            return this;
        }

        /// <summary>
        /// Indicates the application ends.
        /// </summary>
        /// <param name="executionStatus">The execution status to consider.</param>
        public virtual IAppService End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped)
        {
            this._log.End(executionStatus);
            return this;
        }

        /// <summary>
        /// Saves configuration.
        /// </summary>
        public void SaveConfiguration()
        {
            String filePath = this.GetPath(ApplicationPathKind.SettingsFolder) + "appsettings.xml";
            if ((this.UserSettingsSet != null) && (!String.IsNullOrEmpty(filePath)))
                this.UserSettingsSet.SaveXml(filePath);
        }

        /// <summary>
        /// Acknowledges this instance.
        /// </summary>
        /// <returns></returns>
        public virtual Log Acknowledge()
        {
            return new Log();
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        // Credentials -----------------------------

        /// <summary>
        /// Get the credential with the specified name.
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns></returns>
        public ApplicationCredential GetCredential(String name)
        {
            ApplicationCredential credential = new ApplicationCredential
            {
                Name = "[unkwnon]"
            };
            return this.Settings?.Credentials.FirstOrDefault(p => p.KeyEquals(name));
        }

        // Paths --------------------------------------

        /// <summary>
        /// Returns the path of the application temporary folder.
        /// </summary>
        /// <param name="pathKind">The kind of paths.</param>
        /// <returns>The path of the application temporary folder.</returns>
        public virtual String GetPath(ApplicationPathKind pathKind)
        {
            String path = null;
            switch (pathKind)
            {
                case ApplicationPathKind.AppFolder:
                    path = this._appFolderPath;
                    break;
                case ApplicationPathKind.DefaultLogFolder:
                    path = this.GetPath(ApplicationPathKind.RuntimeFolder) + @"logs\";
                    break;
                case ApplicationPathKind.ExtensionsFolder:
                    path = this.Settings.ExtensionsFolderPath;
                    if (String.IsNullOrEmpty(path))
                        path = this.GetPath(ApplicationPathKind.RuntimeFolder) + @"extensions\";
                    break;
                case ApplicationPathKind.LibraryFolder:
                    path = this.GetPath(ApplicationPathKind.RuntimeFolder) + @"lib\";
                    break;
                case ApplicationPathKind.LogFolder:
                    path = this.Settings.LogFolderPath;
                    if (String.IsNullOrEmpty(path))
                        path = this.GetPath(ApplicationPathKind.DefaultLogFolder);
                    break;
                case ApplicationPathKind.RoamingFolder:
                    path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).GetEndedString(@"\");
                    break;
                case ApplicationPathKind.RuntimeFolder:
                    path = this._runtimeFolderPath;
                    if (String.IsNullOrEmpty(path))
                        path = this.Settings?.RuntimeFolderPath;
                    break;
                case ApplicationPathKind.SettingsFile:
                    path = this._settingsFilePath;
                    if (String.IsNullOrEmpty(path))
                        path = this.GetDefaultSettingsFilePath();
                    break;
                case ApplicationPathKind.SettingsFolder:
                    path = this.GetPath(ApplicationPathKind.RuntimeFolder) + @"settings\";
                    break;
                case ApplicationPathKind.TemporaryFolder:
                    path = this.GetPath(ApplicationPathKind.RuntimeFolder) + @"temp\";
                    break;
                case ApplicationPathKind.UsersFolder:
                    path = this.GetPath(ApplicationPathKind.RuntimeFolder) + @"users\";
                    break;
            }

            return (String.IsNullOrEmpty(path) ? StringHelper.__NoneString : path).ToPath();
        }

        #endregion

        // ------------------------------------------
        // LOAD MANAGEMENT
        // ------------------------------------------

        #region Load Management

        /// <summary>
        /// Initializes information.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Returns the log of the task.</returns>
        protected override Log Initialize<T>()
        {
            this._appScope = new T();
            this._appScope.SetAppDomain(AppDomain.CurrentDomain);

            // we initialize the application scope
            this.AppScope.DataContext.AddSystemItem("currentApplicationManager", this);
            this.AppScope.DataSourceService = new DataSourceService();

            Log log = new Log();

            // we load extensions
            log.AddMessage("Loading extensions...");

            // we load the extensions
            this.AppScope.AppExtension.Clear();
            log.Append(
                this.AppScope.AppExtension.LoadLibrary(this._appExtensionConfiguration),
                p => p.HasErrorsOrExceptionsOrWarnings());
            this.AppScope.Update<RuntimeAppScope>();

            try
            {
                // if no errors was found
                if (!log.HasErrorsOrExceptions())
                {
                    log.AddMessage("Extensions loaded");

                    // we load the application settings
                    log.AddMessage("Loading application settings...");

                    String settingsFilePath = this.GetPath(ApplicationPathKind.SettingsFile);

                    if (string.IsNullOrEmpty(settingsFilePath))
                        settingsFilePath = this.GetDefaultSettingsFilePath();

                    AppSettings appSettings = this.LoadSettings(settingsFilePath, log, this._appScope);

                    if (log.HasErrorsOrExceptions())
                    {
                        log.AddError("Could not load application settings");
                    }
                    else
                    {
                        log.AddMessage("Application settings loaded");

                        this.Settings.AppScope = this._appScope;
                        this.Settings.Update(appSettings);
                        this.Settings.Update(new DataElementSpecificationSet(
                            this.SettingsSpecificationSet?.Items?
                                .Where(p =>
                                    p.SpecificationLevels.Has(SpecificationLevel.Definition)
                                    || p.SpecificationLevels.Has(SpecificationLevel.Configuration)).ToArray()),
                            null,
                            new List<UpdateMode> { UpdateMode.Incremental_UpdateCommonItems });

                        if (String.IsNullOrEmpty(this.Settings.ApplicationInstanceName))
                            this.Settings.ApplicationInstanceName = AppService._ApplicationInstanceName;

                        // we update the log folder path

                        this._log.SetFilePath(this.GetPath(ApplicationPathKind.LogFolder), true);

                        //// we load configuration

                        //log.AddMessage("Loading configuration...");

                        //DataElementSet settingsSet = DataItem.Load<DataElementSet>(
                        //    this.GetPath(ApplicationPathKind.SettingsFile), log, null, null, false);
                        //settingsSet?.Update(new DataElementSpecificationSet(
                        //        this.SettingsSpecificationSet.Items
                        //                .Where(p => p.SpecificationLevels.Has(SpecificationLevel.Runtime)).ToArray()),
                        //        null,
                        //        new List<UpdateMode> { UpdateMode.Incremental_UpdateCommonItems });

                        // we build the data module manager
                        this.DataSourceService.Clear();
                        if (this.AppExtension != null)
                        {
                            foreach (DataSource dataSource in this.Settings.DataSources)
                            {
                                this.DataSourceService.AddSource(dataSource);
                            }
                        }

                        if (!log.HasErrorsOrExceptions())
                            log.AddMessage("Application settings loaded");
                    }
                }

                // if at this end, neither exceptions nor errors occured then
                if ((this.GetType() == typeof(AppService)) && (!log.HasErrorsOrExceptions()))
                {
                    // we indicate that the configuration has been well loaded
                    log.AddMessage("Application configuration loaded");
                }
            }
            catch (Exception ex)
            {
                log.AddException(ex);
            }
            finally
            {
            }

            this._isLoadCompleted = !log.HasErrorsOrExceptions();
            if (this.GetType() == typeof(AppService))
                this.LoadComplete();

            return log;
        }

        /// <summary>
        /// Loads the application configuration of this instance.
        /// </summary>
        /// <param name="filePath">The file path to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="xmlSchemaSet">The XML schema set to consider for checking.</param>
        /// <returns>Returns the loading log.</returns>
        public virtual AppSettings LoadSettings(
            String filePath, Log log, IAppScope appScope = null, XmlSchemaSet xmlSchemaSet = null)
        {
            return AppSettings.Load<AppSettings>(filePath, log, appScope, xmlSchemaSet);
        }

        /// <summary>
        /// Fires the 'LoadComplete' event.
        /// </summary>
        public virtual void LoadComplete()
        {
            if (this.OnLoadCompleted != null)
                OnLoadCompleted(this);
        }

        #endregion

        // ------------------------------------------
        // MISCELLANEOUS
        // ------------------------------------------

        #region Miscellaneous

        /// <summary>
        /// Gets the name of the application instance.
        /// </summary>
        /// <returns>Returns the name of the application instance.</returns>
        public static String _ApplicationInstanceName
        {
            get
            {
                return (Environment.MachineName ?? "").ToUpper();
            }
        }

        #endregion
    }
}