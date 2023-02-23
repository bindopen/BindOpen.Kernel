using BindOpen.Data;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using Microsoft.Extensions.Logging;

namespace BindOpen.Hosting.Services
{
    /// <summary>
    /// The interface defines the BindOpen service.
    /// </summary>
    public interface IBdoJob : IIdentified, IBdoScoped
    {
        /// <summary>
        /// The logger.
        /// </summary>
        ILogger Logger { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="logger"></param>
        /// <returns></returns>
        IBdoJob WithLogger(ILogger logger);

        // Execution ---------------------------------

        /// <summary>
        /// The log.
        /// </summary>
        IBdoLog Log { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="log"></param>
        /// <returns></returns>
        IBdoJob WithLog(IBdoLog log);

        // Trigger actions --------------------------------------

        /// <summary>
        /// Indicates whether this is successfully loaded.
        /// </summary>
        bool IsLoaded { get; }

        // Process -----------------------------------

        /// <summary>
        /// The state of the current execution.
        /// </summary>
        ProcessExecutionState ExecutionState { get; }

        /// <summary>
        /// Sets the execution state of this instance.
        /// </summary>
        IBdoJob WithExecutionState(ProcessExecutionState state);

        /// <summary>
        /// The status of the current execution.
        /// </summary>
        ProcessExecutionStatus ExecutionStatus { get; }

        /// <summary>
        /// Sets the execution status of this instance.
        /// </summary>
        IBdoJob WithExecutionStatus(ProcessExecutionStatus status);

        // Process -----------------------------------

        /// <summary>
        /// Starts the service.
        /// </summary>
        /// <returns>Returns the job to consider.</returns>
        IBdoJob Start();

        /// <summary>
        /// Ends the process specifying the status.
        /// </summary>
        /// <param key="executionStatus">The execution status to apply.</param>
        /// <returns>Returns the job to consider.</returns>
        IBdoJob End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped);
    }
}