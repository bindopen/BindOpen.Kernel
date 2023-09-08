using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using BindOpen.Kernel.Scoping;

namespace BindOpen.Kernel.Scoping
{
    /// <summary>
    /// This class represents a DTO entity dico.
    /// </summary>
    [XmlType("EntityDictionary", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "carriers.dico", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
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
