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
    /// The interface defines the BindOpen service.
    /// </summary>
    public interface ITBdoService<S> : IIdentifiedDataItem
        where S : IBdoSettings, new()
    {
        /// <summary>
        /// The settings.
        /// </summary>
        S Settings { get; set; }

        /// <summary>
        /// The loggers.
        /// </summary>
        IBdoLogger[] Loggers { get; set; }

        // Execution ---------------------------------

        /// <summary>
        /// The application domain pool.
        /// </summary>
        AppDomainPool AppDomainPool { get; }

        /// <summary>
        /// The log.
        /// </summary>
        IBdoLog Log { get; }

        // Load --------------------------------------

        /// <summary>
        /// Indicates whether this is successfully loaded.
        /// </summary>
        bool IsSuccessfullyLoaded { get; }

        /// <summary>
        /// Fires the 'LoadComplete' event.
        /// </summary>
        void LoadComplete();

        /// <summary>
        /// This event is triggered when the application is successfully initialized.
        /// </summary>
        event OnLoadCompletedEventHandler OnLoadCompleted;

        // Process -----------------------------------

        /// <summary>
        /// The state of the current execution.
        /// </summary>
        ProcessExecutionState ExecutionState { get; set; }

        /// <summary>
        /// The status of the current execution.
        /// </summary>
        ProcessExecutionStatus ExecutionStatus { get; set; }

        /// <summary>
        /// Starts the service.
        /// </summary>
        /// <returns>Returns the service to consider.</returns>
        ITBdoService<S> Start();

        /// <summary>
        /// Ends the process specifying the status.
        /// </summary>
        /// <param name="executionStatus">The execution status to apply.</param>
        /// <returns>Returns the service to consider.</returns>
        ITBdoService<S> End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped);
    }
}