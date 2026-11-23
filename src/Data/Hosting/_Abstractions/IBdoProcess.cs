using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping;
using System.Threading;
using System.Threading.Tasks;

namespace BindOpen.Hosting
{
    /// <summary>
    /// This interface defines a process that can be executed within the BindOpen framework.
    /// </summary>
    public interface IBdoProcess
    {
        /// <summary>
        /// Executes the process asynchronously.
        /// </summary>
        /// <param name="stoppingToken">The cancellation token to monitor.</param>
        /// <param name="scope">The execution scope containing services and settings.</param>
        /// <param name="varSet">The set of variables available during execution.</param>
        /// <param name="runtimeMode">The mode in which the process is running (e.g., Normal, Simulation).</param>
        /// <param name="log">The log used for tracking execution and errors.</param>
        /// <returns>Returns a task resulting in True if the process completed successfully.</returns>
        Task<bool> ExecuteAsync(
            CancellationToken stoppingToken,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            RuntimeModes runtimeMode = RuntimeModes.Normal,
            IBdoLog log = null);

        /// <summary>
        /// Executes the process synchronously.
        /// </summary>
        /// <param name="stoppingToken">The cancellation token to monitor.</param>
        /// <param name="scope">The execution scope containing services and settings.</param>
        /// <param name="varSet">The set of variables available during execution.</param>
        /// <param name="runtimeMode">The mode in which the process is running (e.g., Normal, Simulation).</param>
        /// <param name="log">The log used for tracking execution and errors.</param>
        /// <returns>Returns True if the process completed successfully.</returns>
        bool Execute(
            CancellationToken stoppingToken,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            RuntimeModes runtimeMode = RuntimeModes.Normal,
            IBdoLog log = null);
    }
}