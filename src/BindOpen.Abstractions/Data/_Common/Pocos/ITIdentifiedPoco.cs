namespace BindOpen.Data.Items
{
    /// <summary>
    /// This interface represents an identified data item.
    /// </summary>
    public interface ITIdentifiedPoco<T> : IIdentified
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        T WithId(string id);
    }
}