using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;

namespace BindOpen.Extensions.Processing
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoTask : IBdoExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="scope"></param>
        /// <param key="varSet">The variable element set to consider.</param>
        /// <param key="runtimeMode"></param>
        /// <param key="log"></param>
        IBdoTask Execute(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            RuntimeModes runtimeMode = RuntimeModes.Normal,
            IBdoLog log = null);
    }
}