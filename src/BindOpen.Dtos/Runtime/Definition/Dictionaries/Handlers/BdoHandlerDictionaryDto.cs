using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents a handler index.
    /// </summary>
    [XmlType("HandlerDictionary", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "handlers.dico", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class BdoHandlerDictionaryDto
        : TBdoExtensionDictionaryDto<BdoHandlerDefinitionDto>
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
        public List<BdoHandlerDefinitionDto> Definitions { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoHandlerDictionaryDto class.
        /// </summary>
        public BdoHandlerDictionaryDto()
        {
        }

        #endregion
    }
}
