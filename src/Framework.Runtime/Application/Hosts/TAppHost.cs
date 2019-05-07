using System;
using System.IO;
using System.Linq;
using System.Xml.Schema;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Core.System.Diagnostics.Loggers.Factories;
using BindOpen.Framework.Core.System.Processing;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.Application.Options;
using BindOpen.Framework.Runtime.Application.Security;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Runtime.Application.Hosts
{
    /// <summary>
    /// This class represents an application host.
    /// </summary>
    public class TAppHost<Q> : TAppService<Q>, ITAppHost<Q>
        where Q : class, IAppConfiguration, new()
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The settings of this instance.
        /// </summary>
        public ITAppSettings<Q> Settings => Options?.Settings;

        /// <summary>
        /// The base options of this instance.
        /// </summary>
        public IBaseAppHostOptions BaseOptions => Options?.Settings as IBaseAppHostOptions;

        /// <summary>
        /// The set of user settings of this intance.
        /// </summary>
        public IDataElementSet UserSettingsSet { get; set; } = new DataElementSet();

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TAppHost class.
        /// </summary>
        public TAppHost() : this(null)
        {
        }

            /// <summary>
            /// Instantiates a new instance of the TAppHost class.
            /// </summary>
            public TAppHost(
            IAppHostScope appScope = null,
            ITAppHostOptions<Q> options = null,
            IDataElementSet userSettingsSet = null) : base(appScope, options)
        {
            // we initiate the options
            Options.SetAppFolder(Directory.GetCurrentDirectory());

            Options.SetExtensions(
                new AppExtensionConfiguration(
                    new AppExtensionFilter("BindOpen.Framework.Runtime"),
                    new AppExtensionFilter("BindOpen.Framework.Runtime")));

            UserSettingsSet = userSettingsSet;
        }

        #endregion

        // ------------------------------------------
        // PROCESSING
        // ------------------------------------------

        #region Processing

        /// <summary>
        /// Configures the application host.
        /// </summary>
        /// <param name="setupOptions">The action to setup the application host.</param>
        /// <returns>Returns the application host.</returns>
        public ITAppHost<Q> Configure(Action<ITAppHostOptions<Q>> setupOptions)
        {
            setupOptions?.Invoke(Options);

            return this;
        }

        /// <summary>
        /// Saves settings.
        /// </summary>
        public void SaveSettings()
        {
            String filePath = GetKnownPath(ApplicationPathKind.SettingsFolder) + "appconfig.xml";
            if ((UserSettingsSet != null) && (!string.IsNullOrEmpty(filePath)))
                UserSettingsSet.SaveXml(filePath);
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
        public IApplicationCredential GetCredential(string name)
        {
            IApplicationCredential credential = new ApplicationCredential
            {
                Name = "[unkwnon]"
            };
            return (Options?.Settings as ITAppSettings<Q>)?.Configuration?.Credentials.Find(p => p.KeyEquals(name));
        }

        // Paths --------------------------------------

        /// <summary>
        /// Returns the path of the application temporary folder.
        /// </summary>
        /// <param name="pathKind">The kind of paths.</param>
        /// <returns>The path of the application temporary folder.</returns>
        public virtual String GetKnownPath(ApplicationPathKind pathKind)
        {
            String path = null;
            switch (pathKind)
            {
                case ApplicationPathKind.AppFolder:
                    path = Options?.AppFolderPath;
                    break;
                case ApplicationPathKind.DefaultLogFolder:
                    path = GetKnownPath(ApplicationPathKind.RuntimeFolder) + @"logs\";
                    break;
                case ApplicationPathKind.ExtensionsFolder:
                    path = Options?.Settings?.ExtensionsFolderPath;
                    if (string.IsNullOrEmpty(path))
                        path = GetKnownPath(ApplicationPathKind.RuntimeFolder) + @"extensions\";
                    break;
                case ApplicationPathKind.LibraryFolder:
                    path = Options?.LibraryFolderPath;
                    if (string.IsNullOrEmpty(path))
                        path = GetKnownPath(ApplicationPathKind.AppFolder);
                    break;
                case ApplicationPathKind.LogFolder:
                    path = Options?.Settings?.LogFolderPath;
                    if (string.IsNullOrEmpty(path))
                        path = GetKnownPath(ApplicationPathKind.DefaultLogFolder);
                    break;
                case ApplicationPathKind.RoamingFolder:
                    path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).GetEndedString(@"\");
                    break;
                case ApplicationPathKind.RuntimeFolder:
                    path = Options?.RuntimeFolderPath;
                    if (string.IsNullOrEmpty(path))
                        path = Options?.Settings?.RuntimeFolderPath;
                    break;
                case ApplicationPathKind.SettingsFile:
                    path = Options?.SettingsFilePath;
                    if (string.IsNullOrEmpty(path))
                        path = GetDefaultSettingsFilePath();
                    break;
                case ApplicationPathKind.SettingsFolder:
                    path = GetKnownPath(ApplicationPathKind.RuntimeFolder) + @"settings\";
                    break;
                case ApplicationPathKind.TemporaryFolder:
                    path = GetKnownPath(ApplicationPathKind.RuntimeFolder) + @"temp\";
                    break;
                case ApplicationPathKind.UsersFolder:
                    path = GetKnownPath(ApplicationPathKind.RuntimeFolder) + @"users\";
                    break;
            }

            return (string.IsNullOrEmpty(path) ? StringHelper.__NoneString : path).ToPath();
        }

        private string GetDefaultSettingsFilePath()
        {
            // by default, settings file is {{runtime folder}}\settings\appconfig.xml
            string defaultSettingsFilePath = string.IsNullOrEmpty(Options?.RuntimeFolderPath) ? null : Options?.RuntimeFolderPath + @"settings\".ToPath() + TAppHostOptions<Q>.__DefaultSettingsFileName;

            if (!File.Exists(defaultSettingsFilePath))
            {
                // by default, settings file is {{runtime folder}}\appconfig.xml
                defaultSettingsFilePath = string.IsNullOrEmpty(Options?.RuntimeFolderPath) ? null : Options?.RuntimeFolderPath + TAppHostOptions<Q>.__DefaultSettingsFileName;
                if (!File.Exists(defaultSettingsFilePath))
                {
                    // then {{application folder}}\app_data\appconfig.xml
                    defaultSettingsFilePath = string.IsNullOrEmpty(Options?.AppFolderPath) ? null : Options?.AppFolderPath + @"app_data\".ToPath() + TAppHostOptions<Q>.__DefaultSettingsFileName;
                    if (!File.Exists(defaultSettingsFilePath))
                    {
                        // then {{application folder}}\appconfig.xml
                        defaultSettingsFilePath = Options?.AppFolderPath + TAppHostOptions<Q>.__DefaultSettingsFileName;
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
        public new virtual ITAppHost<Q> Start(ILog log = null)
        {
            return base.Start(log) as TAppHost<Q>;
        }

        /// <summary>
        /// Indicates the application ends.
        /// </summary>
        /// <param name="executionStatus">The execution status to consider.</param>
        public new virtual ITAppHost<Q> End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped)
        {
            return base.End(executionStatus) as TAppHost<Q>;
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
        protected override ILog Initialize<T>()
        {
            // we initialize logging
            if (Options?.IsDefaultLoggerUsed == true)
            {
                Log.AddLoggers(LoggerFactory.Create<SnapLogger>(
                    Logger.__DefaultName, LoggerMode.Auto, DataSourceKind.Repository, false, null,
                    GetKnownPath(ApplicationPathKind.DefaultLogFolder), "log_" + Id + ".txt"));
            }
            Log.AddLoggers(Options?.Loggers);

            // we initialize as scoped service
            base.Initialize<T>();

            ILog log = new Log();

            // we load extensions
            log.AddMessage("Loading extensions...");

            // we load the extensions
            _appScope.Extension.Clear();
            if (string.IsNullOrEmpty(Options?.ExtensionConfiguration.DefaultFolderPath))
            {
                Options.ExtensionConfiguration.DefaultFolderPath = GetKnownPath(ApplicationPathKind.LibraryFolder);
            }
            log.Append(
                _appScope.AppendLibraries(Options?.ExtensionConfiguration),
                p => p.HasErrorsOrExceptionsOrWarnings());

            _appScope.Update<AppHostScope>();

            try
            {
                // if no errors was found
                if (!log.HasErrorsOrExceptions())
                {
                    log.AddMessage("Extensions loaded");

                    // we load the application settings

                    log.AddMessage("Loading application settings...");

                    String settingsFilePath = GetKnownPath(ApplicationPathKind.SettingsFile);

                    if (string.IsNullOrEmpty(settingsFilePath))
                        settingsFilePath = GetDefaultSettingsFilePath();

                    ITAppSettings<Q> appSettings = LoadSettings(settingsFilePath, _appScope, null, log);

                    if (log.HasErrorsOrExceptions())
                    {
                        log.AddError("Could not load application settings");
                    }
                    else
                    {
                        log.AddMessage("Application settings loaded");

                        Options?.Settings?.SetAppScope(_appScope);
                        Options?.Settings?.Configuration?.Update(appSettings.Configuration);
                        Options?.Settings?.Configuration?.Update(
                            new DataElementSpecSet(
                                Options?.SettingsSpecificationSet?.Items?
                                    .Where(p =>
                                        p.SpecificationLevels?.ToArray().Has(SpecificationLevel.Definition) == true
                                        || p.SpecificationLevels?.ToArray().Has(SpecificationLevel.Configuration) == true).ToArray()),
                                null,
                                new[] { UpdateMode.Incremental_UpdateCommonItems });
                        Options?.Settings?.UpdateRuntimeInfo(_appScope, null, log);

                        if (Options?.Settings != null && string.IsNullOrEmpty(Options.Settings.ApplicationInstanceName))
                        {
                            Options.Settings.ApplicationInstanceName = AppConfiguration._ApplicationInstanceName;
                        }

                        // we update the log folder path

                        Log.SetFilePath(GetKnownPath(ApplicationPathKind.LogFolder), true);

                        //// we load configuration

                        //log.AddMessage("Loading configuration...");

                        //DataElementSet settingsSet = DataItem.Load<DataElementSet>(
                        //    GetPath(ApplicationPathKind.SettingsFile), log, null, null, false);
                        //settingsSet?.Update(new DataElementSpecSet(
                        //        SettingsSpecificationSet.Items
                        //                .Where(p => p.SpecificationLevels.Has(SpecificationLevel.Runtime)).ToArray()),
                        //        null,
                        //        new List<UpdateMode> { UpdateMode.Incremental_UpdateCommonItems });

                        // we build the data module manager
                        DataSourceDepot.Clear();
                        if (_appScope?.Extension != null && Options?.Settings?.Configuration?.DataSources != null)
                        {
                            foreach (IDataSource dataSource in Options?.Settings?.Configuration?.DataSources)
                            {
                                DataSourceDepot.AddSource(dataSource);
                            }
                        }

                        if (!log.HasErrorsOrExceptions())
                            log.AddMessage("Application settings loaded");
                    }
                }

                // if at this end, neither exceptions nor errors occured then
                if ((GetType() == typeof(TAppHost<Q>)) && (!log.HasErrorsOrExceptions()))
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

            _isLoadCompleted = !log.HasErrorsOrExceptions();
            if (GetType() == typeof(TAppHost<Q>))
                LoadComplete();

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
        public virtual ITAppSettings<Q> LoadSettings(
            string filePath,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null,
            XmlSchemaSet xmlSchemaSet = null)
        {
            Q appConfiguration = XmlHelper.Load<Q>(filePath, appScope, scriptVariableSet, log, xmlSchemaSet);
            return new TAppSettings<Q>(appScope, appConfiguration);
        }

        public T GetSettings<T>() where T : TAppSettings<Q>
        {
            return Settings as T;
        }

        #endregion
    }
}