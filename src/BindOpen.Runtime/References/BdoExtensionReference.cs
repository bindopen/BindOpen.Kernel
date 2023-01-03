namespace BindOpen.Runtime.References
{
    /// <summary>
    /// This class represents the BindOpen extension reference.
    /// </summary>
    public class BdoExtensionReference : BdoAssemblyReference, IBdoExtensionReference
    {
        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoExtensionReference class.
        /// </summary>
        public BdoExtensionReference() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoExtensionReference class.
        /// </summary>
        /// <param name="name">The library name to consider.</param>
        /// <param name="version">The library version to consider.</param>
        public BdoExtensionReference(
            string name,
            string version = null) : base(name, version)
        {
        }

        #endregion
    }
}
