using BindOpen.Kernel.Logging;
using BindOpen.Kernel.Scoping;

namespace BindOpen.Kernel.Data.Meta
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
        void SetData(TItem obj);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="scope"></param>
        /// <param key="metaSet"></param>
        /// <param key="log">The BindOpen log used for tracking.</param>
        /// <returns></returns>
        new TItem GetData(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);
    }
}