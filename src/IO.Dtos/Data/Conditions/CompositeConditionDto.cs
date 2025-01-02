using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Conditions;

/// <summary>
/// This class represents an compisite condition DTO.
/// </summary>
[XmlType("CompositeCondition", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
[XmlRoot(ElementName = "condition.composite", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
public class CompositeConditionDto : ConditionDto
{
    // ------------------------------------------
    // PROPERTIES
    // ------------------------------------------

    #region Properties

    /// <summary>
    /// The composite kind of this instance.
    /// </summary>
    [JsonPropertyName("compositionKind")]
    [XmlElement("compositionKind")]
    public BdoCompositeConditionKind CompositionKind { get; set; } = BdoCompositeConditionKind.And;

    /// <summary>
    /// THe conditions of this instance.
    /// </summary>
    [JsonPropertyName("conditions")]
    [XmlArray("conditions")]
    [XmlArrayItem("condition")]
    public List<ConditionDto> Conditions { get; set; }

    #endregion

    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Instantiates a new instance of the CompositeConditionDto class.
    /// </summary>
    public CompositeConditionDto()
    {
    }

    #endregion
}