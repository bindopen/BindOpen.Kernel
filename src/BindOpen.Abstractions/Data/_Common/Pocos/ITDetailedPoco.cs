using BindOpen.Data.Elements;

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
        T WithDetail(IBdoElementSet detail);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="detail"></param>
        T WithDetail(params IBdoElement[] elements);
    }
}
