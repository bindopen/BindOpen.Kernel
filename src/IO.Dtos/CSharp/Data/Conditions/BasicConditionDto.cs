using BindOpen.Data.Meta;
using BindOpen.Scoping.Script;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Conditions
{

    /// <summary>
    /// This class represents a basic condition DTO.
    /// </summary>
    [XmlType("BasicCondition", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "condition", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class BasicConditionDto : ConditionDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The arugment 1 of this instance.
        /// </summary>
        [JsonPropertyName("arg1")]
        [XmlElement("node1", Type = typeof(MetaNodeDto))]
        [XmlElement("object1", Type = typeof(MetaObjectDto))]
        [XmlElement("scalar1", Type = typeof(MetaScalarDto))]
        [XmlElement("word1", Type = typeof(ScriptwordDto))]
        public MetaDataDto Argument1 { get; set; }

        /// <summary>
        /// The operator of this instance.
        /// </summary>
        [JsonPropertyName("operator")]
        [XmlElement("operator")]
        public DataOperators Operator { get; set; }

        /// <summary>
        /// The arugment 2 of this instance.
        /// </summary>
        [JsonPropertyName("arg2")]
        [XmlElement("node2", Type = typeof(MetaNodeDto))]
        [XmlElement("object2", Type = typeof(MetaObjectDto))]
        [XmlElement("scalar2", Type = typeof(MetaScalarDto))]
        [XmlElement("word2", Type = typeof(ScriptwordDto))]
        public MetaDataDto Argument2 { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BasicConditionDto class.
        /// </summary>
        public BasicConditionDto()
        {
        }

        #endregion
    }
}