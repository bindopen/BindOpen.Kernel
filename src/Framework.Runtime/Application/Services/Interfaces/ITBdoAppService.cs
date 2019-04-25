using BindOpen.Framework.Core.System.Assemblies;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Processing;
using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.Application.Hosts.Options;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// The interface defines the application service.
    /// </summary>
    public interface ITBdoAppService<Q> : IScopedService
        where Q : BdoAppConfiguration, new()
    {
        ITBdoAppHostOptions<Q> Options { get; }

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
        event TBdoAppService<Q>.OnLoadCompletedEventHandler OnLoadCompleted;

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
        ITBdoAppService<Q> Start(ILog log = null);

        /// <summary>
        /// Ends the process specifying the status.
        /// </summary>
        /// <param name="executionStatus">The execution status to apply.</param>
        /// <returns>Returns the application host to consider.</returns>
        ITBdoAppService<Q> End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped);
    }
}