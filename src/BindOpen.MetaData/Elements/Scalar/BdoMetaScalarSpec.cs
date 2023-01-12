namespace BindOpen.Meta.Elements
{

    /// <summary>
    /// This class represents a scalar element specification.
    /// </summary>
    public class BdoMetaScalarSpec : BdoMetaElementSpec, IBdoMetaScalarSpec
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ScalarElementSpec class.
        /// </summary>
        public BdoMetaScalarSpec() : base()
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
            var spec = base.Clone(areas) as BdoMetaScalarSpec;
            return spec;
        }

        #endregion
    }

}
