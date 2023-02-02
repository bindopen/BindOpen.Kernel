using BindOpen.Data.Items;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaSpecSet : ITBdoItemSet<IBdoMetaSpec>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        new IBdoMetaSpecSet Add(params IBdoMetaSpec[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        new IBdoMetaSpecSet WithItems(params IBdoMetaSpec[] items);
    }
}