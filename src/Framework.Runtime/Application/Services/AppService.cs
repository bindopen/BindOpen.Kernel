using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.Application.Depots.Datasources;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.System.Assemblies;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Core.System.Processing;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// This class represents an application host.
    /// </summary>
    public abstract class AppService : ScopedService, IAppService
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        /// <summary>
        /// Indicates whether this instance is loaded.
        /// </summary>
        /// <remarks>The value can be assigned.</remarks>
        protected bool _isLoadCompleted = false;

        // Extensions ----------------------

        /// <summary>
        /// This delegate is called when the application is successfully initialized.
        /// </summary>
        /// <param name="sender">The application host</param>
        public delegate void OnLoadCompletedEventHandler(object sender);

        /// <summary>
        /// This event is triggered when the application is successfully initialized.
        /// </summary>
        public event OnLoadCompletedEventHandler OnLoadCompleted;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The runtime folder path.
        /// </summary>
        public ILogger[] Loggers { get; set; }

        /// <summary>
        /// The settings of this instance.
        /// </summary>
        public IBaseSettings Settings { get; set; }

        /// <summary>
        /// The settings of this instance.
        /// </summary>
        public IBaseConfiguration Configuration => Settings.Configuration;

        // Execution ----------------------

        /// <summary>
        /// The current execution state of this instance.
        /// </summary>
        public ProcessExecutionState CurrentExecutionState
        {
            get => Log?.Execution != null ? Log.Execution.State : ProcessExecutionState.None;
            set { if (Log?.Execution != null) Log.Execution.State = value; }
        }

        /// <summary>
        /// The current execution status of this instance.
        /// </summary>
        public ProcessExecutionStatus CurrentExecutionStatus
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
        public ILog Log { get; }

        /// <summary>
        /// Indicates whether the platform information is loaded.
        /// </summary>
        public bool IsLoadCompleted
        {
            get { return _isLoadCompleted; }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the AppService class.
        /// </summary>
        protected AppService(
            IAppHostScope appScope,
            IBaseSettings settings = null,
            ILogger[] loggers = null) : base(appScope)
        {
            Settings = settings;
            Loggers = loggers ?? new ILogger[0];

            // we initiate the log of this instance
            Log = new Log(Loggers)
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
        public virtual IAppService Start(ILog log = null)
        {
            log ??= new Log();

            if (CurrentExecutionState != ProcessExecutionState.Pending)
            {
                // we start the application instance
                log.Start();

                // we initialize this instance
                log.Append(Initialize());

                if (IsLoadCompleted)
                    CurrentExecutionStatus = ProcessExecutionStatus.Stopped_Error;

                log.Sanitize();

                Log?.Append(log);
            }

            return this;
        }

        /// <summary>
        /// Indicates the application ends.
        /// </summary>
        /// <param name="executionStatus">The execution status to consider.</param>
        public virtual IAppService End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped)
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
        /// <typeparam name="A"></typeparam>
        /// <returns>Returns the log of the task.</returns>
        protected override ILog Initialize<A>()
        {
            _appScope = new A();

            // we initialize the application scope
            _appScope.Context.AddSystemItem("appHost", this);
            _appScope.DataSourceDepot = new DataSourceDepot();

            return new Log();
        }

        /// <summary>
        /// Fires the 'LoadComplete' event.
        /// </summary>
        public virtual void LoadComplete()
        {
            OnLoadCompleted?.Invoke(this);
        }

        /// <summary>
        /// Gets the settings of this instance as the specified class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T GetSettings<T>() where T : IBaseSettings
        {
            return (T)Settings;
        }

        #endregion
    }
}