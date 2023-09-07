using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using BindOpen.System.Scoping;

namespace BindOpen.System.Scoping
{
    /// <summary>
    /// This class represents a DTO connector dico.
    /// </summary>
    [XmlType("ConnectorDictionary", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "connectors.dico", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
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
