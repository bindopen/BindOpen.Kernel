using BindOpen.Kernel.Scoping;

namespace BindOpen.Kernel.Scoping
{
    /// <summary>
    /// This class represents a DTO entity dico.
    /// </summary>
    public class BdoEntityDictionary : TBdoExtensionDictionary<IBdoEntityDefinition>, IBdoEntityDictionary
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoEntityDictionary class.
        /// </summary>
        public BdoEntityDictionary()
        {
        }

        #endregion
    }
}
