using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Processing;
using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.Application.Options;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// This class represents an application host.
    /// </summary>
    public class TAppService<Q> : AppService, ITAppService<Q>
        where Q : IAppConfiguration, new()
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private ITAppHostOptions<Q> _options = new TAppHostOptions<Q>();

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        // General ----------------------

            /// <summary>
            /// The options of this instance.
            /// </summary>
        public ITAppHostOptions<Q> Options => _options;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TAppService class.
        /// </summary>
        public TAppService() : this(null, null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TAppService class.
        /// </summary>
        public TAppService(
            IAppHostScope appScope,
            ITAppHostOptions<Q> options) : base(appScope)
        {
            _options = options ?? new TAppHostOptions<Q>();
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

            if (CurrentExecutionState != ProcessExecutionState.Pending)
            {
                // we start the application instance
                log.AddMessage("Starting application instance...");
                log.Start();

                // we initialize this instance
                log.AddMessage("Initializing application...");
                log.Append(Initialize());

                if (!IsLoadCompleted)
                    CurrentExecutionStatus = ProcessExecutionStatus.Stopped_Error;
                else
                    log.AddMessage("Application instance started");

                log.Sanitize();

                Log.Append(log);
            }

            return this;
        }

        /// <summary>
        /// Indicates the application ends.
        /// </summary>
        /// <param name="executionStatus">The execution status to consider.</param>
        public virtual ITAppService<Q> End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped)
        {
            Log.End(executionStatus);
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
            ILog log = base.Initialize<T>();

            if (GetType() == typeof(TAppService<Q>))
                _isLoadCompleted = true;

            return log;
        }

        #endregion
    }
}