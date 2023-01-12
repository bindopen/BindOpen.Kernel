using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents a DTO task dico.
    /// </summary>
    [XmlType("TaskDictionary", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "tasks.dico", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoTaskDictionaryDto : TBdoExtensionDictionaryDto<BdoTaskDefinitionDto>
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
        public List<BdoTaskDefinitionDto> Definitions { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoTaskDictionaryDto class.
        /// </summary>
        public BdoTaskDictionaryDto()
        {
        }

        #endregion
    }
}
