using BindOpen.Framework.Core.Application.Depots.Datasources;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Events;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Core.System.Processing;
using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.Application.Options.Hosts;
using BindOpen.Framework.Runtime.Application.Security;
using BindOpen.Framework.Runtime.Application.Services;
using BindOpen.Framework.Runtime.Application.Settings.Hosts;
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
        where S : class, IBdoHostSettings, new()
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
        public IBdoHostOptions HostOptions { get; set; } = null;

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
        /// Starts the application.
        /// </summary>
        /// <returns>Returns true if this instance is started.</returns>
        public new virtual ITBdoHost<S> Start()
        {
            var log = new BdoLog();

            Process(log);

            Log?.AddMessage("Host starting...");

            Log?.AddSubLog(log, title: "Initializing host...", eventKind: EventKinds.Message);

            if (IsSuccessfullyLoaded)
            {
                Log?.AddMessage("Host started successfully");
            }
            else
            {
                Log?.AddMessage("Host loaded with errors");
                End();
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
                case BdoHostPathKind.AppFolder:
                    path = Options?.AppFolderPath;
                    break;
                case BdoHostPathKind.ConfigurationFile:
                    path = GetKnownPath(BdoHostPathKind.ConfigurationFolder) + BdoDefaultHostPaths.__DefaultConfigurationFileName;
                    break;
                case BdoHostPathKind.ConfigurationFolder:
                    path = Options?.AppSettings?.ConfigurationFolderPath;
                    if (string.IsNullOrEmpty(path))
                    {
                        path = Options?.AppSettings?.ConfigurationFolderPath;
                    }
                    if (string.IsNullOrEmpty(path))
                    {
                        path = GetKnownPath(BdoHostPathKind.RuntimeFolder) + BdoDefaultHostPaths.__DefaultConfigurationFolderPath;
                    }
                    break;
                case BdoHostPathKind.LibraryFolder:
                    path = Options?.AppSettings?.LibraryFolderPath;
                    if (string.IsNullOrEmpty(path))
                    {
                        path = Options?.AppSettings?.LibraryFolderPath;
                    }
                    if (string.IsNullOrEmpty(path))
                    {
                        path = GetKnownPath(BdoHostPathKind.RuntimeFolder) + BdoDefaultHostPaths.__DefaultLibraryFolderPath;
                    }
                    break;
                case BdoHostPathKind.LogsFolder:
                    path = Options?.AppSettings?.LogsFolderPath;
                    if (string.IsNullOrEmpty(path))
                    {
                        path = Options?.AppSettings?.LogsFolderPath;
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
                    path = Options?.AppSettings?.PackagesFolderPath;
                    if (string.IsNullOrEmpty(path))
                    {
                        path = Options?.AppSettings?.PackagesFolderPath;
                    }
                    if (string.IsNullOrEmpty(path))
                    {
                        path = GetKnownPath(BdoHostPathKind.RuntimeFolder) + BdoDefaultHostPaths.__DefaultPackagesFolderPath;
                    }
                    break;
                case BdoHostPathKind.PrimaryLogsFolder:
                    path = GetKnownPath(BdoHostPathKind.RuntimeFolder) + BdoDefaultHostPaths.__DefaultLogsFolderPath;
                    break;
                case BdoHostPathKind.RoamingFolder:
                    path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).GetEndedString(@"\");
                    break;
                case BdoHostPathKind.RuntimeFolder:
                    path = Options?.AppSettings?.RuntimeFolderPath;
                    if (string.IsNullOrEmpty(path))
                    {
                        path = GetKnownPath(BdoHostPathKind.AppFolder) + BdoDefaultHostPaths.__DefaultRuntimeFolderPath;
                    }
                    break;
                case BdoHostPathKind.AppSettingsFile:
                    path = Options.AppSettingsFilePath;
                    if (string.IsNullOrEmpty(path))
                    {
                        path = GetKnownPath(BdoHostPathKind.AppFolder) + BdoDefaultHostPaths.__DefaultAppSettingsFileName;
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
            return Options?.Settings?.HostConfiguration?.Credentials.Find(p => p.KeyEquals(name));
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

            // we update options
            var premiaryAppSettings = Options.AppSettings.Clone<BdoHostAppSettings>();
            Options.Update();

            // we initialize logging
            IBdoLogger primaryLogger = null;
            if (Options?.IsDefaultFileLoggerUsed == true)
            {
                primaryLogger = BdoLoggerFactory.Create<BdoSnapLogger>(
                    BdoLogger.__DefaultName, BdoLoggerMode.Auto, DatasourceKind.Repository, false, null,
                    GetKnownPath(BdoHostPathKind.PrimaryLogsFolder), BdoDefaultHostPaths.__DefaultPrimaryLogsFileNamePreffix + Id + ".txt");
                Log.AddLoggers(primaryLogger);
            }
            Log.AddLoggers(Options?.Loggers?.ToArray());

            // we launch the standard initialization of service

            base.Initialize(log);

            IBdoLog subLog = null;

            // we load the core extensions

            subLog = log.AddSubLog(title: "Loading core extensions...", eventKind: EventKinds.Message);
            var testLog = _scope.LoadExtensions(ExtensionReferenceFactory.CreateRuntime());
            subLog.Append(testLog);
            if (!subLog.HasErrorsOrExceptions())
            {
                subLog.AddMessage("Core extensions loaded");
            }

            // if no errors was found

            if (!log.HasErrorsOrExceptions())
            {
                try
                {
                    // we load the application settings

                    string appSettingsFilePath = GetKnownPath(BdoHostPathKind.AppSettingsFile);
                    Options.AppSettings = premiaryAppSettings ?? new BdoHostAppSettings();

                    if (!File.Exists(appSettingsFilePath))
                    {
                        subLog.AddWarning("File '" + BdoDefaultHostPaths.__DefaultAppSettingsFileName + "' not found");
                    }
                    else
                    {
                        subLog = log.AddSubLog(title: "Loading application settings...", eventKind: EventKinds.Message);
                        subLog.Append(Options.AppSettings.UpdateFromFile(
                                appSettingsFilePath,
                                new SpecificationLevels[] { SpecificationLevels.Definition, SpecificationLevels.Configuration },
                                Options?.SettingsSpecificationSet,
                                _scope, null));
                        if (!subLog.HasErrorsOrExceptions())
                        {
                            subLog.AddMessage("Application settings loaded");
                        }
                    }

                    subLog.Append(Options.Update());

                    if (string.IsNullOrEmpty(Options?.AppSettings.ApplicationInstanceName))
                    {
                        Options.AppSettings.ApplicationInstanceName = BdoHostConfiguration.__ApplicationInstanceName;
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
                        // we load the configuration

                        string configFilePath = GetKnownPath(BdoHostPathKind.ConfigurationFolder) + BdoDefaultHostPaths.__DefaultConfigurationFileName;

                        subLog = log.AddSubLog(title: "Loading configuration...", eventKind: EventKinds.Message);
                        if (!File.Exists(configFilePath))
                        {
                            subLog.AddMessage(title: "No configuration loaded");
                        }
                        else
                        {
                            Options.Settings = new S();
                            Options.Settings.UpdateFromFile(
                                configFilePath,
                                new SpecificationLevels[] { SpecificationLevels.Definition, SpecificationLevels.Configuration },
                                Options?.SettingsSpecificationSet,
                                _scope, null);

                            if (!subLog.HasErrorsOrExceptions())
                            {
                                subLog.AddError("Configuration loaded");
                            }
                        }

                        // we delete expired primary logs

                        int logsExpirationDayNumber = Options?.AppSettings?.LogsExpirationDayNumber ?? -1;
                        primaryLogger?.DeleteExpiredLogs(logsExpirationDayNumber, BdoDefaultHostPaths.__DefaultPrimaryLogsFileNamePreffix + "*.txt");

                        // we update the log folder path

                        Log.SetFilePath(GetKnownPath(BdoHostPathKind.LogsFolder), true, Options?.AppSettings?.LogsFileName);

                        // we delete expired logs

                        foreach (var logger in Log.Loggers)
                        {
                            logger?.DeleteExpiredLogs(logsExpirationDayNumber);
                        }

                        // we populate the data source depot from settings

                        var depot = _scope.DepotSet.Get<IBdoDatasourceDepot>();
                        if (depot != null)
                        {
                            depot.ClearItems();
                            if (_scope?.ExtensionStore != null && Options?.Settings?.HostConfiguration?.Datasources != null)
                            {
                                foreach (Datasource dataSource in Options?.Settings?.HostConfiguration?.Datasources)
                                {
                                    depot.Add(dataSource);
                                }
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
            if (_isLoaded && GetType() == typeof(TBdoHost<S>))
                LoadComplete();
        }

        #endregion
    }
}