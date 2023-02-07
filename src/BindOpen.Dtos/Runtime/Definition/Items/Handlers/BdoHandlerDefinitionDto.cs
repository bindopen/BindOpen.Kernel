using BindOpen.Data.Meta;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents a handler definition.
    /// </summary>
    [XmlType("HandlerDefinition", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "dataHandler.definition", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class BdoHandlerDefinitionDto : BdoExtensionItemDefinitionDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The calling class of this instance.
        /// </summary>
        [JsonPropertyName("callingClass")]
        [XmlElement("callingClass")]
        public string CallingClass { get; set; }

        /// <summary>
        /// Name of the GET function.
        /// </summary>
        [JsonPropertyName("getFunction")]
        [XmlElement("getFunction")]
        public string GetFunctionName { get; set; } = "Get";

        /// <summary>
        /// Name of the POST function.
        /// </summary>
        [JsonPropertyName("postFunction")]
        [XmlElement("postFunction")]
        public string PostFunctionName { get; set; } = "Post";

        // DTO

        /// <summary>
        /// The parameter specification of this instance.
        /// </summary>
        [JsonPropertyName("parameter.specification")]
        [XmlElement("parameter.specification")]
        public SpecListDto ParameterSpecification { get; set; }

        // Source

        /// <summary>
        /// The source object specification of this instance.
        /// </summary>
        [JsonPropertyName("source.specification")]
        [XmlElement("source-object.specification")]
        public ObjectSpecDto SourceObjectSpecification { get; set; }

        /// <summary>
        /// The source scalar specification of this instance.
        /// </summary>
        [JsonPropertyName("source-scalar.specification")]
        [XmlElement("source-scalar.specification")]
        public ScalarSpecDto SourceScalarSpecification { get; set; }

        // Target

        /// <summary>
        /// The target object specification of this instance.
        /// </summary>
        [JsonPropertyName("target.specification")]
        [XmlElement("target-object.specification")]
        public ObjectSpecDto TargetObjectSpecification { get; set; }

        /// <summary>
        /// The target scalar specification of this instance.
        /// </summary>
        [JsonPropertyName("target-scalar.specification")]
        [XmlElement("target-scalar.specification")]
        public ScalarSpecDto TargetScalarSpecification { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoHandlerDefinitionDto class.
        /// </summary>
        public BdoHandlerDefinitionDto()
        {
        }

        #endregion
    }
}
