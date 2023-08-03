using BindOpen.System.Scoping;

namespace BindOpen.System.Scoping
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
