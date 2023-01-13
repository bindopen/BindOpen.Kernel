namespace BindOpen.MetaData
{
    /// <summary>
    /// This interface represents a indexed data.
    /// </summary>
    public interface ITIndexedPoco<T> : IIndexed
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        T WithIndex(int? index);
    }
}
