using BindOpen.Scopes;
using BindOpen.Logging;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoMetaObject<TItem> : IBdoMetaObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="objs"></param>
        ITBdoMetaObject<TItem> WithData(TItem obj);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="scope"></param>
        /// <param key="varSet"></param>
        /// <param key="log"></param>
        /// <returns></returns>
        new TItem GetData(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);
    }
}