using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Definitions
{
    /// <summary>
    /// This class represents a function definition.
    /// </summary>
    [XmlType("FunctionDefinition", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "function.definition", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class BdoFunctionDefinitionDto : ExtensionDefinitionDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Item class of this instance.
        /// </summary>
        [JsonPropertyName("itemClass")]
        [XmlElement("itemClass")]
        public string ItemClass { get; set; }

        /// <summary>
        /// Unit code of this instance.
        /// </summary>
        [JsonPropertyName("unitCode")]
        [XmlElement("unitCode")]
        public string UnitCode { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoFunctionDefinitionDto class. 
        /// </summary>
        public BdoFunctionDefinitionDto()
        {
        }

        #endregion
    }
}