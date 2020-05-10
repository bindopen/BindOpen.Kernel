using System.Xml.Serialization;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// This class represents a DTO connector dictionary.
    /// </summary>
    [XmlType("BdoConnectorDictionary", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "connectors.dictionary", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoConnectorDictionaryDto : TBdoExtensionDictionaryDto<BdoConnectorDefinitionDto>, IBdoConnectorDictionaryDto
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoConnectorDictionaryDto class.
        /// </summary>
        public BdoConnectorDictionaryDto() : base()
        {
        }

        #endregion
    }
}
