using BindOpen.Data.Meta;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Assemblies;

/// <summary>
/// This class represents a class reference DTO.
/// </summary>
[XmlType("ClassReference", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
[XmlRoot(ElementName = "class", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
public class ClassReferenceDto : AssemblyReferenceDto
{
    // --------------------------------------------------
    // PROPERTIES
    // --------------------------------------------------

    #region Properties

    /// <summary>
    /// The class name of this instance.
    /// </summary>
    [JsonPropertyName("className")]
    [XmlElement("className")]
    public string ClassName { get; set; }

    /// <summary>
    /// The class reference of this instance.
    /// </summary>
    [ForeignKey(nameof(MetaDataId))]
    [JsonIgnore]
    [XmlIgnore]
    public MetaDataDto MetaData { get; set; }

    /// <summary>
    /// The class name of this instance.
    /// </summary>
    [JsonIgnore]
    [XmlIgnore]
    public string MetaDataId { get; set; }

    #endregion

    // --------------------------------------------------
    // CONSTRUCTORS
    // --------------------------------------------------

    #region Constructors

    /// <summary>
    /// Instantiates a new instance of the ClassReferenceDto class.
    /// </summary>
    public ClassReferenceDto()
    {
    }

    #endregion
}
