namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a scalar meta that is an meta whose items are scalars.
    /// </summary>
    public class BdoMetaScalar : TBdoMetaScalar<object>,
        IBdoMetaScalar
    {
        // --------------------------------------------------
        // CONVERTERS
        // --------------------------------------------------

        #region Converters

        // String

        /// <summary>
        /// Converts from string.
        /// </summary>
        /// <param key="st">The string to consider.</param>
        public static explicit operator BdoMetaScalar(string st)
            => BdoMeta.NewScalar(DataValueTypes.Any, st);

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param key="meta">The meta to consider.</param>
        public static explicit operator string(BdoMetaScalar meta)
        {
            return meta?.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="items"></param>
        public static implicit operator BdoMetaScalar((string Name, object Value) item)
        {
            var meta = BdoMeta.NewScalar(item.Name, item.Value);

            return meta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="items"></param>
        public static implicit operator BdoMetaScalar((string Name, DataValueTypes ValueType, object Value) item)
        {
            var meta = BdoMeta.NewScalar(item.Name, item.ValueType, item.Value);

            return meta;
        }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ScalarElement class.
        /// </summary>
        public BdoMetaScalar() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ScalarElement class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="id">The ID to consider.</param>
        public BdoMetaScalar(string name = null, string id = null)
            : base(name, id)
        {
        }

        #endregion
    }
}
