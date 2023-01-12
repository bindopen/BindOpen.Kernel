namespace BindOpen.Meta
{
    /// <summary>
    /// This interface represents an named data item.
    /// </summary>
    public interface ITNamedPoco<T> : INamed
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public T WithName(string name);
    }
}