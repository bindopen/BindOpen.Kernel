using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Scoping.Script;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Kernel.Scoping.Functions
{
    /// <summary>
    /// This class represents a function definition DTO.
    /// </summary>
    [XmlType("FunctionDefinition", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "scriptWord.definition", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class FunctionDefinitionDto : ExtensionDefinitionDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The calling class of this instance.
        /// </summary>
        [JsonPropertyName("callingClass")]
        [XmlElement("callingClass")]
        public string CallingClass { get; set; }

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        [JsonPropertyName("kind")]
        [XmlElement("kind")]
        public ScriptItemKinds Kind { get; set; } = ScriptItemKinds.None;

        /// <summary>
        /// The maximum number of parameters of this instance.
        /// </summary>
        [JsonPropertyName("maxParameterNumber")]
        [XmlElement("maxParameterNumber")]
        public int MaxParameterNumber { get; set; } = -1;

        /// <summary>
        /// The minimum number of parameters of this instance.
        /// </summary>
        [JsonPropertyName("minParameterNumber")]
        [XmlElement("minParameterNumber")]
        public int MinParameterNumber { get; set; } = -1;

        /// <summary>
        /// The name of repeated parameters of this instance.
        /// </summary>
        /// <seealso cref="IsRepeatedParameters"/>
        /// <seealso cref="RepeatedParameterValueType"/>
        [JsonPropertyName("repeatedParameterName")]
        [XmlElement("repeatedParameterName")]
        public string RepeatedParameterName { get; set; }

        /// <summary>
        /// The value type of repeated parameters of this instance.
        /// </summary>
        /// <seealso cref="IsRepeatedParameters"/>
        /// <seealso cref="RepeatedParameterName"/>
        [JsonPropertyName("repeatedParameterValueType")]
        [XmlElement("repeatedParameterValueType")]
        public DataValueTypes RepeatedParameterValueType { get; set; }

        /// <summary>
        /// The description of repeated parameters of this instance.
        /// </summary>
        /// <seealso cref="IsRepeatedParameters"/>
        /// <seealso cref="RepeatedParameterName"/>
        [JsonPropertyName("repeatedParameterDescription")]
        [XmlElement("repeatedParameterDescription")]
        public StringDictionaryDto RepeatedParameterDescription { get; set; }

        /// <summary>
        /// The rference unique name of this instance.
        /// </summary>
        [JsonPropertyName("referenceUniqueName")]
        [XmlAttribute("referenceUniqueName")]
        public string ReferenceUniqueName { get; set; }

        /// <summary>
        /// The return value type of this instance.
        /// </summary>
        [JsonPropertyName("returnValueType")]
        [XmlElement("returnValueType")]
        public DataValueTypes ReturnValueType { get; set; } = DataValueTypes.Text;

        /// <summary>
        /// The name of the runtime function.
        /// </summary>
        [JsonPropertyName("functionName")]
        [XmlElement("functionName")]
        public string RuntimeFunctionName { get; set; }

        /// <summary>
        /// The children of this instance.
        /// </summary>
        [JsonPropertyName("children")]
        [XmlArray("children")]
        [XmlArrayItem("add.definition")]
        public List<FunctionDefinitionDto> Children { get; set; }

        /// <summary>
        /// The parameters of this instance.
        /// </summary>
        [JsonPropertyName("parameters")]
        [XmlArray("parameters")]
        [XmlArrayItem("node", Type = typeof(MetaNodeDto))]
        [XmlArrayItem("object", Type = typeof(MetaObjectDto))]
        [XmlArrayItem("scalar", Type = typeof(MetaScalarDto))]
        [XmlArrayItem("word", Type = typeof(ScriptwordDto))]
        public List<MetaDataDto> ParameterSpecification { get; set; }

        /// <summary>
        /// Indicates whether the ParameterSpecification property must be ignored.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public bool ParameterSpecificationSpecified => ParameterSpecification?.Count > 0;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the FunctionDefinitionDto class.
        /// </summary>
        public FunctionDefinitionDto()
        {
        }

        #endregion
    }
}
