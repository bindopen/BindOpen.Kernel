using BindOpen.Framework.Core.Application.Depots.Datasources;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Assemblies;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Core.System.Processing;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// This class represents an bot.
    /// </summary>
    public abstract class TBdoService<S> : IdentifiedDataItem, ITBdoService<S>
        where S : IBdoSettings, new()
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        /// <summary>
        /// Indicates whether this instance is loaded.
        /// </summary>
        /// <remarks>The value can be assigned.</remarks>
        protected bool _isLoaded = false;

        // Extensions ----------------------

        /// <summary>
        /// This event is triggered when the application is successfully initialized.
        /// </summary>
        public event OnLoadCompletedEventHandler OnLoadCompleted;

        // Scope ----------------------

        /// <summary>
        /// The scope of this instance.
        /// </summary>
        protected IBdoScope _scope = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The settings of this instance.
        /// </summary>
        public S Settings { get; set; }

        /// <summary>
        /// The runtime folder path.
        /// </summary>
        public IBdoLogger[] Loggers { get; set; }

        // Execution ----------------------

        /// <summary>
        /// The current execution state of this instance.
        /// </summary>
        public ProcessExecutionState ExecutionState
        {
            get => Log?.Execution != null ? Log.Execution.State : ProcessExecutionState.None;
            set { if (Log?.Execution != null) Log.Execution.State = value; }
        }

        /// <summary>
        /// The current execution status of this instance.
        /// </summary>
        public ProcessExecutionStatus ExecutionStatus
        {
            get => Log?.Execution != null ? Log.Execution.Status : ProcessExecutionStatus.None;
            set { if (Log?.Execution != null) Log.Execution.Status = value; }
        }

        // Loading information ----------------------

        /// <summary>
        /// Application domain pool of this instance.
        /// </summary>
        public AppDomainPool AppDomainPool { get; set; } = null;

        // Tracking ----------------------

        /// <summary>
        /// ILog of this instance.
        /// </summary>
        public IBdoLog Log { get; }

        /// <summary>
        /// Indicates whether the platform information is loaded.
        /// </summary>
        public bool IsSuccessfullyLoaded
        {
            get { return _isLoaded; }
        }

        // Scopes ----------------------

        /// <summary>
        /// The BindOpen extension of this instance.
        /// </summary>
        public IBdoScope Scope
        {
            get { return _scope; }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TBdoService class.
        /// </summary>
        protected TBdoService(
            IBdoScope scope,
            S settings = default,
            IBdoLogger[] loggers = null) : base("")
        {
            _scope = scope;

            Settings = settings;
            Loggers = loggers ?? new IBdoLogger[0];

            // we initiate the log of this instance
            Log = new BdoLog(Loggers)
            {
                Id = Id
            };

            // we instantiate the loaded extension handler and the application script interperter
            AppDomainPool = new AppDomainPool();
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
        public virtual ITBdoService<S> Start()
        {
            var log = new BdoLog();

            Process(log);

            Log?.Append(log);

            return this;
        }

        /// <summary>
        /// Processes the application.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns true if this instance is started.</returns>
        protected virtual ITBdoService<S> Process(IBdoLog log)
        {
            if (ExecutionState != ProcessExecutionState.Pending)
            {
                // we start the application instance
                log?.Start();

                // we initialize this instance
                Initialize(log);

                if (IsSuccessfullyLoaded)
                {
                    ExecutionStatus = ProcessExecutionStatus.Stopped_Error;
                }

                log?.Sanitize();
            }

            return this;
        }

        /// <summary>
        /// Indicates the application ends.
        /// </summary>
        /// <param name="executionStatus">The execution status to consider.</param>
        public virtual ITBdoService<S> End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped)
        {
            Log?.End(executionStatus);
            return this;
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
        protected virtual void Initialize(IBdoLog log)
        {
            // we initialize the application scope

            _scope = BdoScopeFactory.CreateScope();
            _scope.Context.AddSystemItem("host", this);
            _scope.DepotSet.Add(new BdoDatasourceDepot());

            // we initialize the settings

            Settings = new S();

            if (GetType() == typeof(TBdoService<S>))
                _isLoaded = true;
        }

        /// <summary>
        /// Fires the 'LoadComplete' event.
        /// </summary>
        public virtual void LoadComplete()
        {
            OnLoadCompleted?.Invoke(this);
        }

        #endregion
    }
}