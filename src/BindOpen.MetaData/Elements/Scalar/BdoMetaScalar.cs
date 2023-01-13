namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// This class represents a scalar element that is an element whose items are scalars.
    /// </summary>
    public class BdoMetaScalar :
        TBdoMetaElement<IBdoMetaScalar, IBdoMetaScalarSpec, object>,
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
        /// <param name="st">The string to consider.</param>
        public static explicit operator BdoMetaScalar(string st)
            => BdoMeta.NewScalar(DataValueTypes.Any, st) as BdoMetaScalar;

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="element">The element to consider.</param>
        public static explicit operator string(BdoMetaScalar element)
        {
            return element?.ToString();
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
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        public BdoMetaScalar(string name = null, string id = null)
            : base(name, "scalar_", id)
        {
        }

        #endregion

        // --------------------------------------------------
        // ITEMS
        // --------------------------------------------------

        #region Items

        IBdoMetaElementSpec IBdoMetaElement.NewSpecification()
        {
            return NewSpecification();
        }

        // Items ----------------------------

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _item.ToString(ValueType);
        }

        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone(params string[] areas)
        {
            var scalarElement = base.Clone(areas) as BdoMetaScalar;
            return scalarElement;
        }

        #endregion
    }
}
