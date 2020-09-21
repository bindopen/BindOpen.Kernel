using BindOpen.Data.Items;
using BindOpen.System.Assemblies;
using BindOpen.System.Diagnostics;
using BindOpen.System.Processing;

namespace BindOpen.Application.Services
{
    /// <summary>
    /// The interface defines the BindOpen service.
    /// </summary>
    public interface IBdoJob : IIdentifiedDataItem, IBdoScoped
    {
        // Execution ---------------------------------

        /// <summary>
        /// The application domain pool.
        /// </summary>
        AppDomainPool AppDomainPool { get; }

        /// <summary>
        /// The log.
        /// </summary>
        IBdoLog Log { get; }

        // Trigger actions --------------------------------------

        /// <summary>
        /// Indicates whether this is successfully loaded.
        /// </summary>
        bool IsLoaded { get; }

        // Process -----------------------------------

        /// <summary>
        /// The state of the current execution.
        /// </summary>
        ProcessExecutionState ExecutionState { get; set; }

        /// <summary>
        /// The status of the current execution.
        /// </summary>
        ProcessExecutionStatus ExecutionStatus { get; set; }

        // Process -----------------------------------

        /// <summary>
        /// Starts the service.
        /// </summary>
        /// <returns>Returns the service to consider.</returns>
        IBdoJob Start();

        /// <summary>
        /// Ends the process specifying the status.
        /// </summary>
        /// <param name="executionStatus">The execution status to apply.</param>
        /// <returns>Returns the service to consider.</returns>
        IBdoJob End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped);
    }
}