namespace BindOpen.Data.Elements
{

    /// <summary>
    /// This class represents a scalar element specification.
    /// </summary>
    public class ScalarElementSpec : BdoElementSpec, IScalarElementSpec
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ScalarElementSpec class.
        /// </summary>
        public ScalarElementSpec() : base()
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
            var spec = base.Clone(areas) as ScalarElementSpec;
            return spec;
        }

        #endregion
    }

}
