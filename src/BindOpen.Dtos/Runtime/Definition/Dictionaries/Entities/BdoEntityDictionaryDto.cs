using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents a DTO entity dictionary.
    /// </summary>
    [XmlType("EntityDictionary", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "entities.dictionary", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoEntityDictionaryDto
        : TBdoExtensionDictionaryDto<BdoEntityDefinitionDto>
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
        public List<BdoEntityDefinitionDto> Definitions { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoEntityDictionaryDto class.
        /// </summary>
        public BdoEntityDictionaryDto()
        {
        }

        #endregion
    }
}
