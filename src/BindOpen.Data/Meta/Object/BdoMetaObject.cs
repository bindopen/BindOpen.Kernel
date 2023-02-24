namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a catalog el that is an el whose els are carriers.
    /// </summary>
    public partial class BdoMetaObject : TBdoMetaObject<object>,
        IBdoMetaObject
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BdoMetaObject class.
        /// </summary>
        public BdoMetaObject() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the BdoMetaObject class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="id">The ID to consider.</param>
        public BdoMetaObject(string name = null, string id = null)
            : base(name, id)
        {
            this.WithDataValueType(DataValueTypes.Object);
        }

        #endregion
    }
}
