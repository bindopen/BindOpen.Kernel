using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents a DTO routine dico.
    /// </summary>
    [XmlType("RoutineDictionary", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "routines.dico", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoRoutineDictionaryDto
        : TBdoExtensionDictionaryDto<BdoRoutineDefinitionDto>
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
        public List<BdoRoutineDefinitionDto> Definitions { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoRoutineDictionaryDto class.
        /// </summary>
        public BdoRoutineDictionaryDto()
        {
        }

        #endregion
    }
}
