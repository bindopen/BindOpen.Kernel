using BindOpen.Logging;
using BindOpen.Extensions.Scripting;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents a DTO connector dico.
    /// </summary>
    [XmlType("ConnectorDictionary", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "connectors.dico", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class BdoConnectorDictionaryDto
        : TBdoExtensionDictionaryDto<BdoConnectorDefinitionDto>
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
        public List<BdoConnectorDefinitionDto> Definitions { get; set; }

        #endregion

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
