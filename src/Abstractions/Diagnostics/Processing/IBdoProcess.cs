using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Diagnostics.Logging;
using BindOpen.System.Scoping;
using System.Threading;
using System.Threading.Tasks;

namespace BindOpen.System.Diagnostics.Processing
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
        /// <param key="log"></param>
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