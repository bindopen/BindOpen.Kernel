using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Functions
{
    /// <summary>
    /// This class represents a DTO script word dico.
    /// </summary>
    [XmlType("FunctionDictionaryDto", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "scriptwords.dico", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class BdoFunctionDictionaryDto
        : TBdoExtensionDictionaryDto<BdoFunctionDefinitionDto>
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The definition class of this instance.
        /// </summary>
        [JsonPropertyName("definitionClass")]
        [XmlElement("definitionClass")]
        public string DefinitionClass { get; set; }

        /// <summary>
        /// Definitions of this instance.
        /// </summary>
        [JsonPropertyName("definitions")]
        [XmlArray("definitions")]
        [XmlArrayItem("add.definition")]
        public List<BdoFunctionDefinitionDto> Definitions { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoFunctionDictionaryDto class.
        /// </summary>
        public BdoFunctionDictionaryDto()
        {
        }

        #endregion
    }
}
