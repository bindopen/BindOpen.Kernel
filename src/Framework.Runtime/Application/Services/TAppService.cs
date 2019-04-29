using System;
using BindOpen.Framework.Core.Application.Services.Data.Datasources;
using BindOpen.Framework.Core.System.Assemblies;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Processing;
using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.Application.Options;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// This class represents an application host.
    /// </summary>
    public class TAppService<Q> : ScopedService, ITAppService<Q>
        where Q : AppConfiguration, new()
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private ITAppHostOptions<Q> _options = new TAppHostOptions<Q>();

        // General ----------------------

        public ITAppHostOptions<Q> Options => _options;

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
            get { return this._isLoadCompleted; }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoAppService class.
        /// </summary>
        public TAppService() : base()
        {
            // we initiate the log of this instance
            this.Log = new Log(_ => false)
            {
                Id = this.Id
            };

            // we instantiate the loaded extension handler and the application script interperter
            this.AppDomainPool = new AppDomainPool();
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
        public virtual ITAppService<Q> Start(ILog log = null)
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

                this.Log.Append(log);
            }

            return this;
        }

        /// <summary>
        /// Indicates the application ends.
        /// </summary>
        /// <param name="executionStatus">The execution status to consider.</param>
        public virtual ITAppService<Q> End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped)
        {
            this.Log.End(executionStatus);
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
        /// <typeparam name="T"></typeparam>
        /// <returns>Returns the log of the task.</returns>
        protected override ILog Initialize<T>()
        {
            this._appScope = new T();
            this._appScope.Initialize(AppDomain.CurrentDomain);

            // we initialize the application scope
            this.AppScope.DataContext.AddSystemItem("appHost", this);
            this.AppScope.DataSourceService = new DataSourceService();

            if (this.GetType() == typeof(TAppService<Q>))
                this._isLoadCompleted = true;

            return new Log();
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
    }
}