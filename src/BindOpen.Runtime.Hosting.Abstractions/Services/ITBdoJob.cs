using BindOpen.Meta;

namespace BindOpen.Runtime.Services
{
    /// <summary>
    /// The interface defines the BindOpen service.
    /// </summary>
    public interface ITBdoJob<T> : IBdoJob where T : IBdoJob
    {
        /// <summary>
        /// Sets the execution state of this instance.
        /// </summary>
        new T WithExecutionState(ProcessExecutionState state);

        /// <summary>
        /// Sets the execution status of this instance.
        /// </summary>
        new T WithExecutionStatus(ProcessExecutionStatus status);

        // Process -----------------------------------

        /// <summary>
        /// Starts the service.
        /// </summary>
        /// <returns>Returns the service to consider.</returns>
        new T Start();

        /// <summary>
        /// Ends the process specifying the status.
        /// </summary>
        /// <param name="executionStatus">The execution status to apply.</param>
        /// <returns>Returns the service to consider.</returns>
        new T End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped);
    }
}