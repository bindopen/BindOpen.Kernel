using BindOpen.Scopes;
using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Logging;

namespace BindOpen.Extensions.Tasks
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
            IBdoBaseLog log = null);
    }
}