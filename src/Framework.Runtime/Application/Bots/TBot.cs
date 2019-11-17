using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Core.System.Processing;
using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.Application.Options;
using BindOpen.Framework.Runtime.Application.Security;
using BindOpen.Framework.Runtime.Application.Settings;
using BindOpen.Framework.Runtime.System.Diagnostics.Loggers;
using System;
using System.IO;
using System.Linq;

namespace BindOpen.Framework.Runtime.Application.Bots
{
    /// <summary>
    /// This class represents an application host.
    /// </summary>
    public class TBot<T> : Bot, ITBot<T>
        where T : class, IBotSettings, new()
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The application settings.
        /// </summary>
        public new IBotScope Scope => base.Scope as IBotScope;

        /// <summary>
        /// The options of this instance.
        /// </summary>
        public new ITBotOptions<T> Options
        {
            get => base.Options as ITBotOptions<T>;
            set
            {
                base.Options = value;
            }
        }

        /// <summary>
        /// The settings of this instance.
        /// </summary>
        public new T Settings
        {
            get => base.Settings as T;
            set { }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TBot class.
        /// </summary>
        public TBot() : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TBot class.
        /// </summary>
        public TBot(
            IBotScope scope = null,
            ITBotOptions<T> options = null,
            IDataElementSet userSettingsSet = null)
            : base(scope, options ?? new TBotOptions<T>(), userSettingsSet)
        {
            // we initiate the options
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
        public ITBot<T> Configure(Action<ITBotOptions<T>> setupOptions)
        {
            setupOptions?.Invoke(Options);

            return this;
        }

        /// <summary>
        /// Saves settings.
        /// </summary>
        public override void SaveSettings()
        {
            String filePath = GetKnownPath(ApplicationPathKind.SettingsFolder) + BotOptions.__DefaultSettingsFileName;
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
        public override IApplicationCredential GetCredential(string name)
        {
            IApplicationCredential credential = new ApplicationCredential
            {
                Name = "[unkwnon]"
            };
            return (Options?.Settings as T)?.AppConfiguration?.Credentials.Find(p => p.KeyEquals(name));
        }

        // Paths --------------------------------------

        /// <summary>
        /// Returns the path of the application temporary folder.
        /// </summary>
        /// <param name="pathKind">The kind of paths.</param>
        /// <returns>The path of the application temporary folder.</returns>
        public override string GetKnownPath(ApplicationPathKind pathKind)
        {
            string path = null;
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
            // by default, settings file is {{runtime folder}}\settings\bindopen.xml
            string defaultSettingsFilePath = string.IsNullOrEmpty(Options?.RuntimeFolderPath) ? null : Options?.RuntimeFolderPath + @"settings\".ToPath() + TBotOptions<T>.__DefaultSettingsFileName;

            if (!File.Exists(defaultSettingsFilePath))
            {
                // by default, settings file is {{runtime folder}}\bindopen.xml
                defaultSettingsFilePath = string.IsNullOrEmpty(Options?.RuntimeFolderPath) ? null : Options?.RuntimeFolderPath + TBotOptions<T>.__DefaultSettingsFileName;
                if (!File.Exists(defaultSettingsFilePath))
                {
                    // then {{application folder}}\app_data\bindopen.xml
                    defaultSettingsFilePath = string.IsNullOrEmpty(Options?.AppFolderPath) ? null : Options?.AppFolderPath + @"app_data\".ToPath() + TBotOptions<T>.__DefaultSettingsFileName;
                    if (!File.Exists(defaultSettingsFilePath))
                    {
                        // then {{application folder}}\bindopen.xml
                        defaultSettingsFilePath = Options?.AppFolderPath + TBotOptions<T>.__DefaultSettingsFileName;
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
        public new virtual ITBot<T> Start(ILog log = null)
        {
            return base.Start(log) as TBot<T>;
        }

        /// <summary>
        /// Indicates the application ends.
        /// </summary>
        /// <param name="executionStatus">The execution status to consider.</param>
        public new virtual ITBot<T> End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped)
        {
            return base.End(executionStatus) as TBot<T>;
        }

        #endregion

        // ------------------------------------------
        // LOAD MANAGEMENT
        // ------------------------------------------

        #region Load Management

        /// <summary>
        /// Initializes information.
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <returns>Returns the log of the task.</returns>
        protected override ILog Initialize<A>()
        {
            // we initialize logging
            if (Options?.IsDefaultLoggerUsed == true)
            {
                Log.AddLoggers(LoggerFactory.Create<SnapLogger>(
                    Logger.__DefaultName, LoggerMode.Auto, DataSourceKind.Repository, false, null,
                    GetKnownPath(ApplicationPathKind.DefaultLogFolder), "log_" + Id + ".txt"));
            }
            Log.AddLoggers(Options?.Loggers?.ToArray());

            // we initialize as scoped service
            base.Initialize<A>();

            ILog log = new Log();

            // we load extensions
            log.AddMessage("Loading extensions...");

            // we load the extensions
            _scope.Extension.Clear();
            if (string.IsNullOrEmpty(Options?.ExtensionConfiguration.DefaultFolderPath))
            {
                Options.ExtensionConfiguration.DefaultFolderPath = GetKnownPath(ApplicationPathKind.LibraryFolder);
            }
            log.Append(
                _scope.AppendLibraries(Options?.ExtensionConfiguration),
                p => p.HasErrorsOrExceptionsOrWarnings());

            _scope.Update<BotScope>();

            try
            {
                // if no errors was found
                if (!log.HasErrorsOrExceptions())
                {
                    log.AddMessage("Extensions loaded");

                    // we load the application settings

                    log.AddMessage("Loading application settings...");

                    string settingsFilePath = GetKnownPath(ApplicationPathKind.SettingsFile);

                    if (string.IsNullOrEmpty(settingsFilePath))
                        settingsFilePath = GetDefaultSettingsFilePath();

                    Options.Settings = new T();
                    Options.Settings.UpdateFromFile(
                        settingsFilePath,
                        new SpecificationLevels[] { SpecificationLevels.Definition, SpecificationLevels.Configuration },
                        Options?.SettingsSpecificationSet,
                        _scope, null);

                    if (log.HasErrorsOrExceptions())
                    {
                        log.AddError("Could not load application settings");
                    }
                    else
                    {
                        if (Options?.Settings != null && string.IsNullOrEmpty(Options.Settings.ApplicationInstanceName))
                        {
                            Options.Settings.ApplicationInstanceName = BotConfiguration.__ApplicationInstanceName;
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
                        DataSourceDepot.ClearItems();
                        if (_scope?.Extension != null && Options?.Settings?.AppConfiguration?.DataSources != null)
                        {
                            foreach (DataSource dataSource in Options?.Settings?.AppConfiguration?.DataSources)
                            {
                                DataSourceDepot.Add(dataSource);
                            }
                        }

                        if (!log.HasErrorsOrExceptions())
                            log.AddMessage("Application settings loaded");
                    }
                }

                // if at this end, neither exceptions nor errors occured then
                if ((GetType() == typeof(TBot<T>)) && (!log.HasErrorsOrExceptions()))
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

            _isLoaded = !log.HasErrorsOrExceptions();
            if (GetType() == typeof(TBot<T>))
                LoadComplete();

            return log;
        }

        #endregion
    }
}