namespace BindOpen.Data.Meta
{

    /// <summary>
    /// This class represents a scalar element specification.
    /// </summary>
    public class BdoScalarSpec : BdoSpec, IBdoScalarSpec
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ScalarElementSpec class.
        /// </summary>
        public BdoScalarSpec() : base()
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
            var spec = base.Clone(areas) as BdoScalarSpec;
            return spec;
        }

        #endregion
    }

}
