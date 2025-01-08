using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Scoping.Connectors
{
    /// <summary>
    /// This class represents a DTO connector dico.
    /// </summary>
    [XmlType("ConnectorDictionary", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "connector.dico", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class ConnectorDictionaryDto
        : TBdoExtensionDictionaryDto<ConnectorDefinitionDto>
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Definitions of this instance.
        /// </summary>
        [JsonPropertyName("definitions")]
        [XmlArray("definitions")]
        [XmlArrayItem("add.definition")]
        public List<ConnectorDefinitionDto> Definitions { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoConnectorDictionaryDto class.
        /// </summary>
        public ConnectorDictionaryDto() : base()
        {
        }

        #endregion
    }
}
