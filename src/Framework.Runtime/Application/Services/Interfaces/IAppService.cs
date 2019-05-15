using BindOpen.Framework.Core.Application.Configuration;
using BindOpen.Framework.Core.System.Assemblies;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Core.System.Processing;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// The interface defines the application service.
    /// </summary>
    public interface IAppService : IScopedService
    {
        /// <summary>
        /// The loggers.
        /// </summary>
        ILogger[] Loggers { get; set; }

        /// <summary>
        /// The settings.
        /// </summary>
        IBaseSettings Settings { get; set; }

        /// <summary>
        /// The settings.
        /// </summary>
        IBaseConfiguration Configuration { get; }

        // Execution ---------------------------------

        /// <summary>
        /// The application domain pool.
        /// </summary>
        AppDomainPool AppDomainPool { get; }

        /// <summary>
        /// The log.
        /// </summary>
        ILog Log { get; }

        // Load --------------------------------------

        /// <summary>
        /// Indicates whether the load is completed.
        /// </summary>
        bool IsLoadCompleted { get; }

        /// <summary>
        /// Fires the 'LoadComplete' event.
        /// </summary>
        void LoadComplete();

        /// <summary>
        /// This event is triggered when the application is successfully initialized.
        /// </summary>
        event AppService.OnLoadCompletedEventHandler OnLoadCompleted;

        // Process -----------------------------------

        /// <summary>
        /// The state of the current execution.
        /// </summary>
        ProcessExecutionState CurrentExecutionState { get; set; }

        /// <summary>
        /// The status of the current execution.
        /// </summary>
        ProcessExecutionStatus CurrentExecutionStatus { get; set; }

        /// <summary>
        /// Starts the process.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the application host to consider.</returns>
        IAppService Start(ILog log = null);

        /// <summary>
        /// Ends the process specifying the status.
        /// </summary>
        /// <param name="executionStatus">The execution status to apply.</param>
        /// <returns>Returns the application host to consider.</returns>
        IAppService End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped);

        T GetSettings<T>() where T : IBaseSettings;
    }
}