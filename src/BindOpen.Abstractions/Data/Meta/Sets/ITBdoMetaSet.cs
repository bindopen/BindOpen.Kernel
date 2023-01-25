namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoMetaSet<T> : IBdoMetaSet
        where T : IBdoMetaSet
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        new T FromObject(object obj);

        /// <summary>
        /// 
        /// </summary>
        new T ClearItems();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new T Add(params IBdoMetaData[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new T WithItems(params IBdoMetaData[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        new T Remove(params string[] keys);
    }
}