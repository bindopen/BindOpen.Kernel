using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Logging;
using BindOpen.Kernel.Scoping;
using System.Threading;
using System.Threading.Tasks;

namespace BindOpen.Kernel.Processing
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoProcess
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="scope"></param>
        /// <param key="varSet">The variable element set to consider.</param>
        /// <param key="runtimeMode"></param>
        /// <param key="log">The BindOpen log used for tracking.</param>
        Task<bool> ExecuteAsync(
            CancellationToken stoppingToken,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            RuntimeModes runtimeMode = RuntimeModes.Normal,
            IBdoLog log = null);

        bool Execute(
            CancellationToken stoppingToken,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            RuntimeModes runtimeMode = RuntimeModes.Normal,
            IBdoLog log = null);
    }
}