using BindOpen.Framework.Application.Scopes;
using BindOpen.Framework.Data.Items;
using BindOpen.Framework.System.Assemblies;
using BindOpen.Framework.System.Diagnostics;
using BindOpen.Framework.System.Diagnostics.Loggers;
using BindOpen.Framework.System.Processing;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Framework.Application.Services
{
    /// <summary>
    /// This class represents an bot.
    /// </summary>
    public abstract class BdoService : IdentifiedDataItem, IBdoService
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
        /// Indicates whether the platform information is loaded.
        /// </summary>
        public bool IsLoaded
        {
            get { return _isLoaded; }
        }

        /// <summary>
        /// The runtime folder path.
        /// </summary>
        public List<IBdoLogger> Loggers { get; set; }

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

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoService class.
        /// </summary>
        protected BdoService(
            IBdoScope scope,
            params IBdoLogger[] loggers) : base("")
        {
            _scope = scope;

            Loggers = loggers?.ToList() ?? new List<IBdoLogger>();

            // we initiate the log of this instance
            Log = new BdoLog(Loggers.ToArray())
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

        // Methods ------------------------------

        /// <summary>
        /// Starts the application.
        /// </summary>
        /// <returns>Returns true if this instance is started.</returns>
        public virtual IBdoService Start()
        {
            var log = new BdoLog();

            Process(log);

            log.AddEventsTo(Log);

            return this;
        }

        /// <summary>
        /// Processes the application.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns true if this instance is started.</returns>
        protected virtual IBdoService Process(IBdoLog log)
        {
            if (ExecutionState != ProcessExecutionState.Pending)
            {
                // we start the application instance
                log?.Start();

                // we initialize this instance
                Initialize(log);

                log?.Sanitize();
            }

            return this;
        }

        /// <summary>
        /// Indicates the application ends.
        /// </summary>
        /// <param name="executionStatus">The execution status to consider.</param>
        public virtual IBdoService End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped)
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
            _scope.Context.AddSystemItem("bdoHost", this);
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
                _scope?.Dispose();
            }
        }

        #endregion
    }
}