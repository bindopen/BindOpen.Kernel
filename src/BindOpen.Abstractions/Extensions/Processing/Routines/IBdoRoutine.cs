using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;

namespace BindOpen.Extensions.Processing
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoRoutine : IBdoExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="scope"></param>
        /// <param key="varSet">The variable element set to consider.</param>
        /// <param key="item"></param>
        /// <param key="dataElement"></param>
        /// <param key="objects"></param>
        /// <returns></returns>
        IBdoRoutine Execute(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            object item = null,
            IBdoMetaData dataElement = null,
            IBdoLog log = null,
            params object[] objects);
    }
}