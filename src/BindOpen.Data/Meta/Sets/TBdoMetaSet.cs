namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    public abstract class TBdoMetaSet<T> : BdoMetaSet,
        ITBdoMetaSet<T>
        where T : IBdoMetaSet
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the TBdoElement class.
        /// </summary>
        public TBdoMetaSet() : base()
        {
        }

        #endregion


        // --------------------------------------------------
        // ITBdoMetaSet<T> implementation
        // --------------------------------------------------

        #region ITBdoMetaSet<T>

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public new T FromObject(object obj)
            => (T)base.FromObject(obj);

        /// <summary>
        /// 
        /// </summary>
        public new T ClearItems()
            => (T)base.ClearItems();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public new T Add(params IBdoMetaData[] items)
            => (T)base.Add(items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public new T WithItems(params IBdoMetaData[] items)
            => (T)base.WithItems(items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        public new T Remove(params string[] keys)
            => (T)base.Remove(keys);

        #endregion
    }
}
