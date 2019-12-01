using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Processing;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// The interface defines the service service.
    /// </summary>
    public interface IBdoService : IIdentifiedDataItem
    {
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

        // Scope -----------------------------------

        /// <summary>
        /// The application scope.
        /// </summary>
        IBdoScope Scope { get; }
    }
}