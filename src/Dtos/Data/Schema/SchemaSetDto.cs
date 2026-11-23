using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Schema;

/// <summary>
/// This class represents a meta set DTO.
/// </summary>
[XmlType("SchemaSet", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
[XmlRoot("schemaSet", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
public class SchemaSetDto : IBdoDto
{
    // -------------------------------------------------------
    // PROPERTIES
    // -------------------------------------------------------

    #region Properties

    /// <summary>
    /// ID of this instance.
    /// </summary>
    [JsonPropertyName("id")]
    [XmlAttribute("id")]
    public string Identifier { get; set; }

    /// <summary>
    /// Name of this instance.
    /// </summary>
    [JsonPropertyName("name")]
    [XmlAttribute("name")]
    [DefaultValue(null)]
    public string Name { get; set; }

    /// <summary>
    /// The elements of this instance.
    /// </summary>
    [NotMapped]
    [JsonPropertyName("items")]
    [XmlElement("schema", Type = typeof(SchemaDto))]
    public List<SchemaDto> Items { get; set; }

    #endregion

    // -------------------------------------------------------------
    // CONSTRUCTORS
    // -------------------------------------------------------------

    #region Constructors

    /// <summary>
    /// Instantiates a new instance of the SpecSetDto class.
    /// </summary>
    public SchemaSetDto() : base()
    {
    }

    #endregion
}
