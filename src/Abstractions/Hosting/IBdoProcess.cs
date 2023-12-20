using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping;
using System.Threading;
using System.Threading.Tasks;

namespace BindOpen.Hosting
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
        /// <param key="metaSet">The variable element set to consider.</param>
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