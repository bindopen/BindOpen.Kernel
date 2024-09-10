using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Conditions;

/// <summary>
/// This class represents an expression condition DTO.
/// </summary>
[XmlType("ExpressionCondition", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
[XmlRoot(ElementName = "condition.expression", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
public class ExpressionConditionDto : ConditionDto
{
    // ------------------------------------------
    // PROPERTIES
    // ------------------------------------------

    #region Properties

    /// <summary>
    /// The expression of this instance.
    /// </summary>
    [ForeignKey(nameof(ExpressionItemId))]
    [JsonPropertyName("expression")]
    [XmlElement("expression")]
    public ExpressionDto ExpressionItem { get; set; }

    /// <summary>
    /// The expression of this instance.
    /// </summary>
    [JsonIgnore]
    [XmlIgnore]
    public string ExpressionItemId { get; set; }

    #endregion

    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Instantiates a new instance of the ExpressionConditionDto class.
    /// </summary>
    public ExpressionConditionDto()
    {
    }

    #endregion
}