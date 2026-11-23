using BindOpen.Data.Conditions;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Schema;

/// <summary>
/// This class represents a data key value.
/// </summary>
[XmlType("rule", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
[XmlRoot(ElementName = "rule", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
public class SchemaRuleDto
{
    // --------------------------------------------------
    // PROPERTIES
    // --------------------------------------------------

    #region Properties

    /// <summary>
    /// Identifier of this instance.
    /// </summary>
    [JsonPropertyName("id")]
    [XmlAttribute("id")]
    public string Identifier { get; set; }

    /// <summary>
    /// The kind of this instance.
    /// </summary>
    [JsonPropertyName("kind")]
    [XmlAttribute("kind")]
    [DefaultValue(BdoSchemaRuleKinds.None)]
    public BdoSchemaRuleKinds Kind { get; set; }

    /// <summary>
    /// The group identifier of this instance.
    /// </summary>
    [JsonPropertyName("groupId")]
    [XmlElement("groupId")]
    public string GroupId { get; set; }

    /// <summary>
    /// Values of this instance.
    /// </summary>
    [JsonPropertyName("value")]
    [XmlElement("value")]
    public MetaScalarDto Value { get; set; }

    /// <summary>
    /// The reference of this instance.
    /// </summary>
    [JsonPropertyName("reference")]
    [XmlElement("reference")]
    public ReferenceDto Reference { get; set; }

    /// <summary>
    /// Default items of this instance.
    /// </summary>
    [JsonPropertyName("condition")]
    [XmlElement("condition", Type = typeof(BasicConditionDto))]
    [XmlElement("condition.composite", Type = typeof(CompositeConditionDto))]
    [XmlElement("condition.expression", Type = typeof(ExpressionConditionDto))]
    public ConditionDto Condition { get; set; }

    /// <summary>
    /// The result code of this instance.
    /// </summary>
    [JsonPropertyName("resultCode")]
    [XmlElement("resultCode")]
    public string ResultCode { get; set; }

    [JsonPropertyName("resultEventLevel")]
    [XmlElement("resultEventLevel")]
    [DefaultValue(BdoEventKinds.None)]
    public BdoEventKinds ResultEventLevel { get; set; }

    [JsonPropertyName("resultTitle")]
    [XmlElement("resultTitle")]
    public string ResultTitle { get; set; }

    [JsonPropertyName("resultDescription")]
    [XmlElement("resultDescription")]
    public string ResultDescription { get; set; }

    #endregion

    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Instantiates a new instance of SchemaRuleDto class.
    /// </summary>
    public SchemaRuleDto()
    {
    }

    #endregion
}
