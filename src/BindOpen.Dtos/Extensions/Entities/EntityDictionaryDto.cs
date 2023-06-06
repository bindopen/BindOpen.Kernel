using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Entities
{
    /// <summary>
    /// This class represents a DTO entity dico.
    /// </summary>
    [XmlType("EntityDictionary", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "carriers.dico", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class EntityDictionaryDto
        : TBdoExtensionDictionaryDto<EntityDefinitionDto>
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
        public List<EntityDefinitionDto> Definitions { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoEntityDictionaryDto class.
        /// </summary>
        public EntityDictionaryDto()
        {
        }

        #endregion
    }
}
