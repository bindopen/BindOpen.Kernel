using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents a DTO entity dico.
    /// </summary>
    [XmlType("EntityDictionary", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "entities.dico", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
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
