using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Definitions
{
    /// <summary>
    /// This class represents a DTO function dico.
    /// </summary>
    [XmlType("FunctionDictionary", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "function.dico", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class BdoFunctionDictionaryDto
        : TBdoExtensionDictionaryDto<BdoFunctionDefinitionDto>
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
