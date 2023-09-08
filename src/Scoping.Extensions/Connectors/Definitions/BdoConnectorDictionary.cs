using BindOpen.Kernel.Scoping;

namespace BindOpen.Kernel.Scoping
{
    /// <summary>
    /// This class represents a DTO connector dico.
    /// </summary>
    public class BdoConnectorDictionary : TBdoExtensionDictionary<IBdoConnectorDefinition>, IBdoConnectorDictionary
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoConnectorDictionary class.
        /// </summary>
        public BdoConnectorDictionary() : base()
        {
        }

        #endregion
    }
}
