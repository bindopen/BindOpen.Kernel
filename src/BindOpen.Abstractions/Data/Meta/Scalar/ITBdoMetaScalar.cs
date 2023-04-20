using BindOpen.Logging;
using BindOpen.Scopes;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoMetaScalar<TItem> : IBdoMetaScalar
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="objs"></param>
        void SetData(TItem obj);

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
            IBdoBaseLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="scope"></param>
        /// <param key="varSet"></param>
        /// <param key="log"></param>
        /// <returns></returns>
        new TItem GetData(
            int index,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoBaseLog log = null);
    }
}