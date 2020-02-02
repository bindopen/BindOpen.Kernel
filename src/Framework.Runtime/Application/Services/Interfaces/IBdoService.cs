using BindOpen.Framework.Data.Items;
using BindOpen.Framework.System.Assemblies;
using BindOpen.Framework.System.Diagnostics;
using BindOpen.Framework.System.Diagnostics.Loggers;
using BindOpen.Framework.System.Processing;
using System.Collections.Generic;

namespace BindOpen.Framework.Application.Services
{
    /// <summary>
    /// The interface defines the BindOpen service.
    /// </summary>
    public interface IBdoService : IIdentifiedDataItem
    {
        /// <summary>
        /// The loggers.
        /// </summary>
        List<IBdoLogger> Loggers { get; set; }

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
        IBdoService Start();

        /// <summary>
        /// Ends the process specifying the status.
        /// </summary>
        /// <param name="executionStatus">The execution status to apply.</param>
        /// <returns>Returns the service to consider.</returns>
        IBdoService End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped);
    }
}