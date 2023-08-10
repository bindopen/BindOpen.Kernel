using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a meta set DTO.
    /// </summary>
    [XmlType("SpecSet", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot("specSet", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class SpecSetDto : IBdoDto
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
        public string Id { get; set; }

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
        [JsonPropertyName("items")]
        [XmlElement("spec", Type = typeof(SpecDto))]
        [XmlElement("spec.aggregate", Type = typeof(AggregateSpecDto))]
        public List<SpecDto> Items { get; set; }

        #endregion

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the SpecSetDto class.
        /// </summary>
        public SpecSetDto() : base()
        {
        }

        #endregion
    }
}
