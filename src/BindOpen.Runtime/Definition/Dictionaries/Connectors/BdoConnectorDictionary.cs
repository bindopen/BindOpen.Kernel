using BindOpen.Extensions.Connecting;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents a DTO connector dictionary.
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
