using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Events;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Core.System.Processing;
using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.Application.Options;
using BindOpen.Framework.Runtime.Application.Security;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Runtime.Application.Settings;
using BindOpen.Framework.Runtime.Extensions;
using BindOpen.Framework.Runtime.System.Diagnostics.Loggers;
using System;
using System.IO;
using System.Linq;

namespace BindOpen.Framework.Runtime.Application.Hosts
{
    /// <summary>
    /// This class represents an application host.
    /// </summary>
    public class TBdoHost<S> : TBdoService<S>, ITBdoHost<S>
        where S : class, IBdoAppSettings, new()
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The application settings.
        /// </summary>
        public new IBdoScope Scope => base.Scope as IBdoScope;

        /// <summary>
        /// The options of this instance.
        /// </summary>
        public IBdoHostOptions HostOptions => Options;

        /// <summary>
        /// The options of this instance.
        /// </summary>
        public ITBdoHostOptions<S> Options { get; set; } = null;

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
        /// Instantiates a new instance of the TBdoHost class.
        /// </summary>
        public TBdoHost() : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TBdoHost class.
        /// </summary>
        public TBdoHost(
            IBdoScope scope = null,
            ITBdoHostOptions<S> options = null,
            IDataElementSet userSettingsSet = null)
             : base(scope, options?.Settings)
        {
            Options = options;

            UserSettingsSet = userSettingsSet;
        }

        #endregion

        // ------------------------------------------
        // PROCESSING
        // ------------------------------------------

        #region Processing

        /// <summary>
        /// Runs the specified action.
        /// </summary>
        /// <param name="action">The action to consider.</param>
        /// <returns>Returns this instance.</returns>
        public ITBdoHost<S> Run(Action<ITBdoHost<S>> action)
        {
            action?.Invoke(this);

            return this;
        }

        /// <summary>
        /// Starts the application.
        /// </summary>
        /// <returns>Returns true if this instance is started.</returns>
        public new virtual ITBdoHost<S> Start()
        {
            var log = new BdoLog();

            Process(log);

            Log?.AddMessage("Host starting...");

            Log?.AddSubLog(log, title: "Initializing host...", eventKind: EventKinds.Message);

            if (IsLoaded)
            {
                Log?.AddMessage("Host started successfully");
                StartSucceeds();
            }
            else
            {
                Log?.AddMessage("Host loaded with errors");
                End();
                StartFails();
            }

            return this;
        }

        /// <summary>
        /// Processes the application.
        /// </summary>
        /// <returns>Returns true if this instance is started.</returns>
        protected new virtual ITBdoHost<S> Process(IBdoLog log = null)
        {
            return base.Process(log) as TBdoHost<S>;
        }

        /// <summary>
        /// Indicates the application ends.
        /// </summary>
        /// <param name="executionStatus">The execution status to consider.</param>
        public new virtual ITBdoHost<S> End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped)
        {
            // we unload the host (syncrhonously for the moment)
            _isLoaded = false;
            _scope.Clear();

            Log?.AddMessage("Host ended");
            return base.End(executionStatus) as ITBdoHost<S>;
        }

        /// <summary>
        /// Configures the application host.
        /// </summary>
        /// <param name="setupOptions">The action to setup the application host.</param>
        /// <returns>Returns the application host.</returns>
        public ITBdoHost<S> Configure(Action<ITBdoHostOptions<S>> setupOptions)
        {
            Options = Options ?? new TBdoHostOptions<S>();
            setupOptions?.Invoke(Options);

            return this;
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        // Paths --------------------------------------

        /// <summary>
        /// Returns the path of the application temporary folder.
        /// </summary>
        /// <param name="pathKind">The kind of paths.</param>
        /// <returns>The path of the application temporary folder.</returns>
        public string GetKnownPath(BdoHostPathKind pathKind)
        {
            string path = null;
            switch (pathKind)
            {
                case BdoHostPathKind.RootFolder:
                    path = Options?.RootFolderPath;
                    break;
                case BdoHostPathKind.AppConfigFile:
                    path = GetKnownPath(BdoHostPathKind.ConfigurationFolder) + BdoDefaultHostPaths.__DefaultAppConfigFileName;
                    break;
                case BdoHostPathKind.ConfigurationFolder:
                    path = Options?.HostSettings?.AppConfigurationFolderPath;
                    if (string.IsNullOrEmpty(path))
                    {
                        path = Options?.HostSettings?.AppConfigurationFolderPath;
                    }
                    if (string.IsNullOrEmpty(path))
                    {
                        path = GetKnownPath(BdoHostPathKind.RuntimeFolder) + BdoDefaultHostPaths.__DefaultAppConfigFolderPath;
                    }
                    break;
                case BdoHostPathKind.LibraryFolder:
                    path = Options?.HostSettings?.LibraryFolderPath;
                    if (string.IsNullOrEmpty(path))
                    {
                        path = Options?.HostSettings?.LibraryFolderPath;
                    }
                    if (string.IsNullOrEmpty(path))
                    {
                        path = GetKnownPath(BdoHostPathKind.RuntimeFolder) + BdoDefaultHostPaths.__DefaultLibraryFolderPath;
                    }
                    break;
                case BdoHostPathKind.LogsFolder:
                    path = Options?.HostSettings?.LogsFolderPath;
                    if (string.IsNullOrEmpty(path))
                    {
                        path = Options?.HostSettings?.LogsFolderPath;
                    }
                    if (string.IsNullOrEmpty(path))
                    {
                        path = GetKnownPath(BdoHostPathKind.RuntimeFolder) + BdoDefaultHostPaths.__DefaultLogsFolderPath;
                    }
                    if (string.IsNullOrEmpty(path))
                    {
                        path = GetKnownPath(BdoHostPathKind.PrimaryLogsFolder);
                    }
                    break;
                case BdoHostPathKind.PackagesFolder:
                    path = Options?.HostSettings?.PackagesFolderPath;
                    if (string.IsNullOrEmpty(path))
                    {
                        path = Options?.HostSettings?.PackagesFolderPath;
                    }
                    if (string.IsNullOrEmpty(path))
                    {
                        path = GetKnownPath(BdoHostPathKind.RuntimeFolder) + BdoDefaultHostPaths.__DefaultPackagesFolderPath;
                    }
                    break;
                case BdoHostPathKind.ProjectsFolder:
                    path = Options?.HostSettings?.ProjectsFolderPath;
                    if (string.IsNullOrEmpty(path))
                    {
                        path = Options?.HostSettings?.ProjectsFolderPath;
                    }
                    if (string.IsNullOrEmpty(path))
                    {
                        path = GetKnownPath(BdoHostPathKind.RuntimeFolder) + BdoDefaultHostPaths.__DefaultProjectsFolderPath;
                    }
                    break;
                case BdoHostPathKind.PrimaryLogsFolder:
                    path = GetKnownPath(BdoHostPathKind.RuntimeFolder) + BdoDefaultHostPaths.__DefaultLogsFolderPath;
                    break;
                case BdoHostPathKind.RoamingFolder:
                    path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).GetEndedString(@"\");
                    break;
                case BdoHostPathKind.RuntimeFolder:
                    path = Options?.HostSettings?.RuntimeFolderPath;
                    if (string.IsNullOrEmpty(path))
                    {
                        path = GetKnownPath(BdoHostPathKind.RootFolder) + BdoDefaultHostPaths.__DefaultRuntimeFolderPath;
                    }
                    break;
                case BdoHostPathKind.HostConfigFile:
                    path = Options.HostConfigFilePath;
                    if (string.IsNullOrEmpty(path))
                    {
                        path = GetKnownPath(BdoHostPathKind.RootFolder) + BdoDefaultHostPaths.__DefaultHostConfigFileName;
                    }
                    break;
                case BdoHostPathKind.TemporaryFolder:
                    path = GetKnownPath(BdoHostPathKind.RuntimeFolder) + @"temp\";
                    break;
                case BdoHostPathKind.UsersFolder:
                    path = GetKnownPath(BdoHostPathKind.RuntimeFolder) + @"users\";
                    break;
            }

            return (string.IsNullOrEmpty(path) ? StringHelper.__NoneString : path).ToPath();
        }

        // Credentials -----------------------------

        /// <summary>
        /// Get the credential with the specified name.
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns></returns>
        public virtual IApplicationCredential GetCredential(string name)
        {
            IApplicationCredential credential = new ApplicationCredential
            {
                Name = "[unkwnon]"
            };
            return Options?.Settings?.AppConfiguration?.Credentials.Find(p => p.KeyEquals(name));
        }

        #endregion

        // ------------------------------------------
        // LOAD MANAGEMENT
        // ------------------------------------------

        #region Load Management

        /// <summary>
        /// Initializes information.
        /// </summary>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the log of the task.</returns>
        protected override void Initialize(IBdoLog log)
        {
            log = log ?? new BdoLog();

            // we bind the trigger actions

            var options = Options as TBdoHostOptions<S>;

            Action_OnExecutionSucess = options?.Action_OnExecutionSucess;
            Action_OnExecutionFailure = options?.Action_OnExecutionFailure;
            Action_OnStartSuccess = options?.Action_OnStartSuccess;
            Action_OnStartFailure = options?.Action_OnStartFailure;

            // we clone the current options host settings as the primary ones

            var primaryHostSettings = Options.HostSettings?.Clone<BdoHostSettings>();

            // we determine the root folder path

            var rootFolderPathDefinition = Options?.RootFolderPathDefinitions?.FirstOrDefault(p => p.Predicate(Options) == true);
            if (rootFolderPathDefinition != null)
            {
                Options?.SetRootFolder(rootFolderPathDefinition?.RootFolderPath);
            }

            // we update options (specially paths)

            Options.Update();

            // we initialize logging

            IBdoLogger primaryLogger = null;
            if (Options?.DefaultLoggerOutputKinds?.Count > 0)
            {
                primaryLogger = BdoLoggerFactory.Create<BdoSnapLogger>(BdoLogger.__DefaultName, BdoLoggerMode.Auto);

                if (Options.DefaultLoggerOutputKinds.Contains(DatasourceKind.Repository))
                {
                    primaryLogger.AddFileOutput(GetKnownPath(BdoHostPathKind.PrimaryLogsFolder), BdoDefaultHostPaths.__DefaultPrimaryLogsFileNamePreffix + Id + ".txt");
                }

                if (Options.DefaultLoggerOutputKinds.Contains(DatasourceKind.Console))
                {
                    primaryLogger.AddConsoleOutput();
                }

                Log.AddLoggers(primaryLogger);
            }

            Log.AddLoggers(Options?.Loggers?.ToArray());

            // we launch the standard initialization of service

            base.Initialize(log);

            IBdoLog subLog = null;

            // we load the core extensions

            subLog = log.AddSubLog(title: "Loading core extensions...", eventKind: EventKinds.Message);
            subLog.Append(_scope.LoadExtensions(ExtensionReferenceFactory.CreateRuntime()));
            if (!subLog.HasErrorsOrExceptions())
            {
                subLog.AddMessage("Core extensions loaded");
            }

            // if no errors was found

            if (!log.HasErrorsOrExceptions())
            {
                try
                {
                    // we load the host configuration

                    string hostConfigFilePath = GetKnownPath(BdoHostPathKind.HostConfigFile);
                    Options.HostSettings = primaryHostSettings ?? new BdoHostSettings();

                    if (!File.Exists(hostConfigFilePath))
                    {
                        var message = "Host configuration file ('" + BdoDefaultHostPaths.__DefaultHostConfigFileName + "') not found";
                        if (Options.IsHostConfigFileRequired)
                        {
                            subLog.AddError(message);
                        }
                        else
                        {
                            subLog.AddWarning(message);
                        }
                    }
                    else
                    {
                        subLog = log.AddSubLog(title: "Loading host configuration...", eventKind: EventKinds.Message);
                        subLog.Append(Options.HostSettings.UpdateFromFile(
                                hostConfigFilePath,
                                new SpecificationLevels[] { SpecificationLevels.Definition, SpecificationLevels.Configuration },
                                Options?.SettingsSpecificationSet,
                                _scope, null));
                        if (!subLog.HasErrorsOrExceptions())
                        {
                            subLog.AddMessage("Host configuration loaded");
                        }
                    }

                    subLog.Append(Options.Update());

                    if (string.IsNullOrEmpty(Options?.HostSettings.ApplicationInstanceName))
                    {
                        Options.HostSettings.ApplicationInstanceName = BdoAppConfiguration.__ApplicationInstanceName;
                    }

                    // we load extensions

                    subLog = log.AddSubLog(title: "Loading extensions...", eventKind: EventKinds.Message);

                    if (Options?.ExtensionReferences.Count == 0)
                    {
                        subLog.AddMessage("No extensions found");
                    }
                    else
                    {
                        Options.ExtensionLoadOptions?.WithLibraryFolderPath(GetKnownPath(BdoHostPathKind.LibraryFolder));
                        foreach (var reference in Options?.ExtensionReferences)
                        {
                            subLog.Append(_scope.LoadExtensions(p => p.Update(Options?.ExtensionLoadOptions), reference), l => l.HasErrorsOrExceptionsOrWarnings());
                        }

                        if (!subLog.HasErrorsOrExceptions())
                        {
                            subLog.AddMessage("Extensions loaded");
                        }
                    }

                    if (!log.HasErrorsOrExceptions())
                    {
                        // we load the application configuration

                        Options.Settings = new S();

                        string appConfigFilePath = GetKnownPath(BdoHostPathKind.ConfigurationFolder) + BdoDefaultHostPaths.__DefaultAppConfigFileName;

                        subLog = log.AddSubLog(title: "Loading application configuration...", eventKind: EventKinds.Message);
                        if (!File.Exists(appConfigFilePath))
                        {
                            var message = "Application configuration file ('" + BdoDefaultHostPaths.__DefaultAppConfigFileName + "') not found";
                            if (Options.HostSettings.IsAppConfigFileRequired)
                            {
                                subLog.AddError(message);
                            }
                            else
                            {
                                subLog.AddWarning(message);
                            }
                        }
                        else
                        {
                            subLog.Append(Options.Settings.UpdateFromFile(
                                appConfigFilePath,
                                new SpecificationLevels[] { SpecificationLevels.Definition, SpecificationLevels.Configuration },
                                Options?.SettingsSpecificationSet,
                                _scope, null));
                        }
                        if (!subLog.HasErrorsOrExceptions())
                        {
                            subLog.AddMessage("Application configuration loaded");
                        }
                        else
                        {
                            subLog.AddMessage(title: "No configuration loaded");
                        }

                        // we delete expired primary logs

                        int logsExpirationDayNumber = Options?.HostSettings?.LogsExpirationDayNumber ?? -1;
                        primaryLogger?.DeleteExpiredLogs(logsExpirationDayNumber, BdoDefaultHostPaths.__DefaultPrimaryLogsFileNamePreffix + "*.txt");

                        // we update the log folder path

                        Log.ForLoggers(p => p.AddFileOutput(GetKnownPath(BdoHostPathKind.LogsFolder), Options?.HostSettings?.LogsFileName, true));

                        // we delete expired logs in the logs folder

                        foreach (var logger in Log.Loggers)
                        {
                            logger?.DeleteExpiredLogs(logsExpirationDayNumber);
                        }

                        // we load the data store

                        _scope.DataStore = Options?.DataStore;
                        subLog = log.AddSubLog(title: "Loading data store...", eventKind: EventKinds.Message);
                        if (_scope.DataStore == null)
                        {
                            subLog.AddMessage(title: "No data store registered");
                        }
                        else
                        {
                            _scope.DataStore.LoadLazy(subLog);

                            if (!subLog.HasErrorsOrExceptions())
                            {
                                subLog.AddMessage("Data store loaded (" + _scope.DataStore.Depots.Count + " depots added)");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.AddException(ex);
                }
                finally
                {
                }
            }

            _isLoaded = !log.HasErrorsOrExceptions();
        }

        #endregion
    }
}