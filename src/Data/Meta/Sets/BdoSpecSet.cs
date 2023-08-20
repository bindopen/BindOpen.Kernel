using BindOpen.System.Data.Helpers;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a catalog el that is an el whose els are entities.
    /// </summary>
    public partial class BdoSpecSet : TBdoSet<IBdoSpec>, IBdoSpecSet
    {
        // ------------------------------------------
        // CONVERTERS
        // ------------------------------------------

        #region Converters

        /// <summary>
        /// Converts from data element array.
        /// </summary>
        /// <param key="elems">The elems to consider.</param>
        public static explicit operator BdoSpecSet(IBdoSpec[] elems)
        {
            return BdoData.NewSpecSet(elems);
        }

        /// <summary>
        /// Converts from data element array.
        /// </summary>
        /// <param key="elems">The elems to consider.</param>
        public static explicit operator IBdoSpec[](BdoSpecSet metaSet)
        {
            return metaSet?.ToArray();
        }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BdoSpecSet class.
        /// </summary>
        public BdoSpecSet() : base()
        {
        }

        #endregion

        // ------------------------------------------
        // INamed Implementation
        // ------------------------------------------

        #region INamed

        /// <summary>
        /// 
        /// </summary>
        [BdoProperty("name")]
        public string Name { get; set; }

        #endregion

        // --------------------------------------------------
        // IBdoSpecSet Implementation
        // --------------------------------------------------

        #region IBdoSpecSet

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Empty;
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
        public override object Clone()
        {
            var el = base.Clone().As<BdoSpecSet>();

            return el;
        }

        #endregion
    }
}
