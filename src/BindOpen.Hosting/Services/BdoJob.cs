using BindOpen.Data;
using BindOpen.Data;
using BindOpen.Logging;
using BindOpen.Scopes;
using BindOpen.Scopes.Application;
using BindOpen.Scopes.Scopes;
using Microsoft.Extensions.Logging;

namespace BindOpen.Hosting.Services
{
    /// <summary>
    /// This class represents an bot.
    /// </summary>
    public abstract class BdoJob : BdoItem, IBdoJob
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoJob class.
        /// </summary>
        protected BdoJob(IBdoLog log) : base()
        {
            // we instantiate the loaded extension handler and the application script interperter
            AppDomainPool = new AppDomainPool();
        }

        #endregion

        // ------------------------------------------
        // IIdentified Implementation
        // ------------------------------------------

        #region IIdentified

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        #endregion

        // ------------------------------------------
        // INamed Implementation
        // ------------------------------------------

        #region INamed

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        #endregion

        // ------------------------------------------
        // IBdoJob Implementation
        // ------------------------------------------

        #region IBdoJob

        /// <summary>
        /// Indicates whether this instance is loaded.
        /// </summary>
        /// <remarks>The value can be assigned.</remarks>
        protected bool _isLoaded = false;

        /// <summary>
        /// Indicates whether the platform information is loaded.
        /// </summary>
        public bool IsLoaded
        {
            get { return _isLoaded; }
        }

        /// <summary>
        /// The scope of this instance.
        /// </summary>
        public IBdoScope Scope { get; set; }

        public IBdoJob WithLog(IBdoLog log)
        {
            Log = log;

            return this;
        }


        public IBdoScoped WithScope(IBdoScope scope)
        {
            Scope = scope;

            return this;
        }

        /// <summary>
        /// The logger of this instance.
        /// </summary>
        public ILogger Logger { get; private set; }

        // Execution ----------------------

        /// <summary>
        /// The current execution state of this instance.
        /// </summary>
        public ProcessExecutionState ExecutionState
        {
            get => Log?.Execution?.State ?? ProcessExecutionState.None;
        }

        /// <summary>
        /// The current execution status of this instance.
        /// </summary>
        public ProcessExecutionStatus ExecutionStatus
        {
            get => Log?.Execution?.Status ?? ProcessExecutionStatus.None;
        }

        // Loading information ----------------------

        /// <summary>
        /// Application domain pool of this instance.
        /// </summary>
        public IAppDomainPool AppDomainPool { get; private set; }

        // Tracking ----------------------

        /// <summary>
        /// ILog of this instance.
        /// </summary>
        public IBdoLog Log { get; private set; }

        /// <summary>
        /// Starts the application.
        /// </summary>
        /// <returns>Returns true if this instance is started.</returns>
        public IBdoJob WithLogger(ILogger logger)
        {
            Logger = logger;

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="state"></param>
        /// <returns></returns>
        public IBdoJob WithExecutionState(ProcessExecutionState state)
        {
            var execution = Log?.Execution;
            if (execution != null)
            {
                execution.State = execution?.State ?? ProcessExecutionState.None;
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="status"></param>
        /// <returns></returns>
        public IBdoJob WithExecutionStatus(ProcessExecutionStatus status)
        {
            var execution = Log?.Execution;
            if (execution != null)
            {
                execution.Status = execution?.Status ?? ProcessExecutionStatus.None;
            }

            return this;
        }

        // Methods ------------------------------

        /// <summary>
        /// Starts the application.
        /// </summary>
        /// <returns>Returns true if this instance is started.</returns>
        public virtual IBdoJob Start()
        {
            Process();

            return this;
        }

        /// <summary>
        /// Processes the application.
        /// </summary>
        /// <param key="log">The log to consider.</param>
        /// <returns>Returns true if this instance is started.</returns>
        protected virtual IBdoJob Process()
        {
            if (ExecutionState != ProcessExecutionState.Pending)
            {
                // we start the application instance
                Log?.Start();

                // we initialize this instance
                Initialize();

                Log?.Sanitize();
            }

            return this;
        }

        /// <summary>
        /// Indicates the application ends.
        /// </summary>
        /// <param key="executionStatus">The execution status to consider.</param>
        public virtual IBdoJob End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped)
        {
            Log?.End(executionStatus);
            return this;
        }

        /// <summary>
        /// Initializes information.
        /// </summary>
        /// <returns>Returns the log of the task.</returns>
        protected virtual void Initialize()
        {
            // we initialize the application scope

            Scope = BdoScoping.NewScope();
            Scope.Context.AddSystemItem("bdoHost", this);

            _isLoaded = true;
        }

        #endregion
    }
}