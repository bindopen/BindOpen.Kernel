using BindOpen.Meta.Items;

namespace BindOpen.Meta.Elements
{
    /// <summary>
    /// This class represents a set of data element specifications.
    /// </summary>
    public class BdoMetaElementSpecSet : TBdoItemSet<IBdoMetaElementSpec>, IBdoElementSpecSet
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BdoElementSpecSet class.
        /// </summary>
        public BdoMetaElementSpecSet() : base()
        {
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
            return base.Clone();
        }

        #endregion
    }

}
