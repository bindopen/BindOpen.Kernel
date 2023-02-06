using BindOpen.Data.Items;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaSpecList : ITBdoList<IBdoMetaSpec>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        new IBdoMetaSpecList Add(params IBdoMetaSpec[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        new IBdoMetaSpecList With(
            params IBdoMetaSpec[] items);
    }
}