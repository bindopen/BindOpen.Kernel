using BindOpen.Data.Meta;
using BindOpen.Scoping.Script;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Conditions;


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
    [ForeignKey(nameof(ArgumentMetaData1Id))]
    [JsonPropertyName("arg1")]
    [XmlElement("node1", Type = typeof(MetaNodeDto))]
    [XmlElement("object1", Type = typeof(MetaObjectDto))]
    [XmlElement("scalar1", Type = typeof(MetaScalarDto))]
    [XmlElement("word1", Type = typeof(ScriptwordDto))]
    public MetaDataDto ArgumentMetaData1 { get; set; }

    /// <summary>
    /// The class name of this instance.
    /// </summary>
    [JsonIgnore]
    [XmlIgnore]
    public string ArgumentMetaData1Id { get; set; }

    /// <summary>
    /// The operator of this instance.
    /// </summary>
    [JsonPropertyName("operator")]
    [XmlElement("operator")]
    public DataOperators Operator { get; set; }

    /// <summary>
    /// The arugment 2 of this instance.
    /// </summary>
    [ForeignKey(nameof(ArgumentMetaData2Id))]
    [JsonPropertyName("arg2")]
    [XmlElement("node2", Type = typeof(MetaNodeDto))]
    [XmlElement("object2", Type = typeof(MetaObjectDto))]
    [XmlElement("scalar2", Type = typeof(MetaScalarDto))]
    [XmlElement("word2", Type = typeof(ScriptwordDto))]
    public MetaDataDto ArgumentMetaData2 { get; set; }

    /// <summary>
    /// The class name of this instance.
    /// </summary>
    [JsonIgnore]
    [XmlIgnore]
    public string ArgumentMetaData2Id { get; set; }

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