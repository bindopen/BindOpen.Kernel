using BindOpen.MetaData.Elements;

namespace BindOpen.MetaData
{
    /// <summary>
    /// This interface represents a named data.
    /// </summary>
    public interface ITDetailedPoco<T> : IDetailed
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="detail"></param>
        T WithDetail(IBdoMetaElementSet detail);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="detail"></param>
        T WithDetail(params IBdoMetaElement[] elements);
    }
}
