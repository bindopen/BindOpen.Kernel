using BindOpen.Kernel.Logging;
using BindOpen.Kernel.Scoping;

namespace BindOpen.Kernel.Data.Meta
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
        /// <param key="log">The BindOpen log used for tracking.</param>
        /// <returns></returns>
        new TItem GetData(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="scope"></param>
        /// <param key="varSet"></param>
        /// <param key="log">The BindOpen log used for tracking.</param>
        /// <returns></returns>
        new TItem GetData(
            int index,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);
    }
}