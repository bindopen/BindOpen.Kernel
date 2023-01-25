using BindOpen.Extensions.Scripting;
using BindOpen.Logging;
using BindOpen.Data;
using BindOpen.Data.Context;
using BindOpen.Data.Stores;
using BindOpen.Runtime.References;
using BindOpen.Runtime.Scopes;
using BindOpen.Runtime.Services;
using BindOpen.Runtime.Stores;
using System;
using System.IO;
using System.Linq;

namespace BindOpen.Runtime.Hosts
{
    /// <summary>
    /// This class represents a host.
    /// </summary>
    public class BdoHost : BdoJob, IBdoHost
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoHost class.
        /// </summary>
        /// <param name="log"></param>
        public BdoHost(IBdoLog log) : base(log)
        {
        }

        #endregion

        // ------------------------------------------
        // IBdoHost Implementation
        // ------------------------------------------

        #region IBdoHost

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public new IBdoHost WithExecutionState(ProcessExecutionState state)
        {
            return base.WithExecutionState(state) as IBdoHost;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public new IBdoHost WithExecutionStatus(ProcessExecutionStatus status)
        {
            return base.WithExecutionStatus(status) as IBdoHost;
        }

        /// <summary>
        /// The options of this instance.
        /// </summary>
        public IBdoHostOptions Options { get; private set; }

        /// <summary>
        /// Sets the specfied options
        /// </summary>
        /// <param name="options"></param>
        /// <returns>Returns this instance.</returns>
        public IBdoHost WithOptions(IBdoHostOptions options)
        {
            Options = options;

            return this;
        }

        /// <summary>
        /// Runs the specified action.
        /// </summary>
        /// <param name="action">The action to consider.</param>
        /// <returns>Returns this instance.</returns>
        public IBdoHost Run(Action<IBdoHost> action)
        {
            action?.Invoke(this);

            return this;
        }

        /// <summary>
        /// Starts the application.
        /// </summary>
        /// <returns>Returns true if this instance is started.</returns>
        public new virtual IBdoHost Start()
        {
            Process();

            Log?.AddMessage("Host starting...");

            Initialize();

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
        protected new virtual IBdoHost Process()
        {
            return base.Process() as IBdoHost;
        }

        /// <summary>
        /// Indicates the application ends.
        /// </summary>
        /// <param name="executionStatus">The execution status to consider.</param>
        public new virtual IBdoHost End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped)
        {
            // we unload the host (syncrhonously for the moment)
            _isLoaded = false;
            Clear();

            Log?.AddMessage("Host ended");
            return base.End(executionStatus) as IBdoHost;
        }

        /// <summary>
        /// Configures the application host.
        /// </summary>
        /// <param name="setupOptions">The action to setup the application host.</param>
        /// <returns>Returns the application host.</returns>
        public IBdoHost Configure(Action<IBdoHostOptions> setupOptions)
        {
            Options ??= new BdoHostOptions();
            setupOptions?.Invoke(Options);

            return this;
        }

        // Trigger actions --------------------------------------

        /// <summary>
        /// Indicates that this instance has successfully started.
        /// </summary>
        private void StartSucceeds()
        {
            Options?.Action_OnStartSuccess?.Invoke(this);
        }

        /// <summary>
        /// Indicates that this instance has not successfully started.
        /// </summary>
        private void StartFails()
        {
            Options?.Action_OnStartFailure?.Invoke(this);
        }

        /// <summary>
        /// Indicates that this instance completes.
        /// </summary>
        private void ExecutionSucceeds()
        {
            Options?.Action_OnExecutionSucess?.Invoke(this);
        }

        /// <summary>
        /// Indicates that this instance fails.
        /// </summary>
        private void ExecutionFails()
        {
            Options?.Action_OnExecutionFailure?.Invoke(this);
        }

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
                case BdoHostPathKind.LibraryFolder:
                    path = Options?.Settings?.LibraryFolderPath;
                    if (string.IsNullOrEmpty(path))
                    {
                        path = Options?.Settings?.LibraryFolderPath;
                    }
                    if (string.IsNullOrEmpty(path))
                    {
                        path = GetKnownPath(BdoHostPathKind.RootFolder) + BdoDefaultHostPaths.__DefaultLibraryFolderPath;
                    }
                    break;
                case BdoHostPathKind.HostConfigFile:
                    path = Options.SettingsFilePath;
                    if (string.IsNullOrEmpty(path))
                    {
                        path = GetKnownPath(BdoHostPathKind.RootFolder) + BdoDefaultHostPaths.__DefaultHostConfigFileName;
                    }
                    break;
            }

            return (string.IsNullOrEmpty(path) ? null : path).ToPath();
        }

        /// <summary>
        /// Initializes information.
        /// </summary>
        /// <returns>Returns the log of the task.</returns>
        protected override void Initialize()
        {
            // we determine the root folder path

            var rootFolderPathDefinition = Options?.RootFolderPathDefinitions?.FirstOrDefault(p => p.Predicate(Options) == true);
            if (rootFolderPathDefinition != null)
            {
                Options?.SetRootFolder(rootFolderPathDefinition?.RootFolderPath);
            }

            // we update options (specially paths)

            //Options.Update();

            // we set the logger

            //Log.WithLogger(Options.LoggerInit?.Invoke(this));

            // we launch the standard initialization of service
            var log = Log?.InsertSubLog(title: "Initializing host...", eventKind: EventKinds.Message);

            IBdoLog subLog = null;

            base.Initialize();

            // if no errors was found

            if (_isLoaded)
            {
                try
                {
                    // we load the host configuration

                    string hostConfigFilePath = GetKnownPath(BdoHostPathKind.HostConfigFile);

                    if (!File.Exists(hostConfigFilePath))
                    {
                        var message = "Host configuration file ('" + BdoDefaultHostPaths.__DefaultHostConfigFileName + "') not found";
                        if (Options.IsSettingsFileRequired == true)
                        {
                            log?.AddError(message);
                            _isLoaded = false;
                        }
                        else if (Options.IsSettingsFileRequired == false)
                        {
                            log?.AddWarning(message);
                        }
                    }
                    else
                    {
                        subLog = log?.InsertSubLog(title: "Loading host configuration...", eventKind: EventKinds.Message);
                        //Options.Settings.UpdateFromFile(
                        //        hostConfigFilePath,
                        //        new SpecificationLevels[] { SpecificationLevels.Definition, SpecificationLevels.Configuration },
                        //        null, _scope, null).AddEventsTo(log);
                        if (subLog?.HasEvent(EventKinds.Error, EventKinds.Exception) != true)
                        {
                            subLog?.AddMessage("Host configuration loaded");
                        }
                    }

                    //Options.Update().AddEventsTo(subLog);

                    // we load extensions

                    subLog = log?.InsertSubLog(title: "Loading extensions...", eventKind: EventKinds.Message);

                    if (Options?.ExtensionReferences.Count == 0)
                    {
                        subLog?.AddMessage("No extensions found");
                    }
                    else
                    {
                        Options.ExtensionLoadOptions?.WithLibraryFolderPath(GetKnownPath(BdoHostPathKind.LibraryFolder));
                        foreach (var reference in Options?.ExtensionReferences)
                        {
                            _isLoaded &= Scope.LoadExtensions(
                                p =>
                                {
                                    p.Update(Options?.ExtensionLoadOptions);
                                    return true;
                                },
                                new[] { reference },
                                subLog);
                        }

                        if (subLog?.HasEvent(EventKinds.Error, EventKinds.Exception) != true)
                        {
                            subLog?.AddMessage("Extensions loaded");
                        }
                    }

                    if (_isLoaded)
                    {
                        // we load the data store

                        Scope.Clear();

                        if (Options?.DataStore != null)
                        {
                            foreach (var dataStore in Options.DataStore.Depots)
                            {
                                Scope.DataStore.Add(dataStore.Value);
                            }
                        }

                        subLog = log?.InsertSubLog(title: "Loading data store...", eventKind: EventKinds.Message);
                        if (Scope.DataStore == null)
                        {
                            subLog?.AddMessage(title: "No data store registered");
                        }
                        else
                        {
                            Scope.DataStore.LoadLazy(this, subLog);

                            if (subLog?.HasEvent(EventKinds.Error, EventKinds.Exception) != true)
                            {
                                subLog?.AddMessage("Data store loaded (" + Scope.DataStore.Depots.Count + " depots added)");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    log?.AddException(ex);
                }
                finally
                {
                }
            }
        }

        #endregion

        // --------------------------------------------------
        // IDisposable IBdoScoped
        // --------------------------------------------------

        #region IDisposable Implementation

        /// <summary>
        /// The application domain of this instance.
        /// </summary>
        public AppDomain AppDomain => Scope?.AppDomain;

        /// <summary>
        /// The extension store of this instance.
        /// </summary>
        public IBdoExtensionStore ExtensionStore => Scope?.ExtensionStore;

        /// <summary>
        /// The data store of this instance.
        /// </summary>
        public IBdoDataStore DataStore => Scope?.DataStore;

        /// <summary>
        /// The context of this instance.
        /// </summary>
        public IBdoDataContext Context => Scope?.Context;

        /// <summary>
        /// Loads the specified extensions.
        /// </summary>
        /// <param name="references">The extension references to consider.</param>
        /// <param name="log">The log to consider.</param>
        public bool LoadExtensions(
            IBdoAssemblyReference[] references,
            IBdoLog log = null)
            => Scope?.LoadExtensions(references, log) ?? false;

        public bool LoadExtensions(
            params IBdoAssemblyReference[] references)
            => LoadExtensions(references, null);

        /// <summary>
        /// Loads the specified extensions.
        /// </summary>
        /// <param name="loadOptionsAction">The load options action to consider.</param>
        /// <param name="references">The extension references to consider.</param>
        /// <param name="log">The log to consider.</param>
        public bool LoadExtensions(
            Func<IExtensionLoadOptions, bool> loadOptionsAction,
            IBdoAssemblyReference[] references,
            IBdoLog log = null)
            => Scope?.LoadExtensions(loadOptionsAction, references, log) ?? false;

        public bool LoadExtensions(
            Func<IExtensionLoadOptions, bool> loadOptionsAction,
            params IBdoAssemblyReference[] references)
            => LoadExtensions(loadOptionsAction, references, null);

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
            => Scope?.Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IBdoScriptInterpreter NewScriptInterpreter()
        {
            return Scope?.NewScriptInterpreter();
        }

        #endregion
    }
}