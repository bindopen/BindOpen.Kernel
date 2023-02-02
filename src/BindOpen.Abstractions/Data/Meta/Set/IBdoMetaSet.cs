using BindOpen.Data.Items;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public partial interface IBdoMetaSet :
        ITBdoItemSet<IBdoMetaItem>,
        IBdoMetaItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        new IBdoMetaSet Add(
            params IBdoMetaItem[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        new IBdoMetaSet WithItems(
            params IBdoMetaItem[] items);
    }
}