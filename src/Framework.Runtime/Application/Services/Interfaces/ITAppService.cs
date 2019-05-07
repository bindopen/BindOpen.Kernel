using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Processing;
using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.Application.Options;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// The interface defines the application service.
    /// </summary>
    public interface ITAppService<Q> : IAppService
        where Q : IAppConfiguration, new()
    {
        ITAppHostOptions<Q> Options { get; }

        /// <summary>
        /// Starts the process.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the application host to consider.</returns>
        ITAppService<Q> Start(ILog log = null);

        /// <summary>
        /// Ends the process specifying the status.
        /// </summary>
        /// <param name="executionStatus">The execution status to apply.</param>
        /// <returns>Returns the application host to consider.</returns>
        ITAppService<Q> End(ProcessExecutionStatus executionStatus = ProcessExecutionStatus.Stopped);
    }
}