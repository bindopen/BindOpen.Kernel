using BindOpen.Data.Meta;

namespace BindOpen.Data
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
        T WithDetail(IBdoMetaSet detail);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="detail"></param>
        T WithDetail(params IBdoMetaData[] elements);
    }
}
