using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.System.Data.Apis
{
    /// <summary>
    /// This class represents a Api log DTO.
    /// </summary>
    [XmlType("ApiLogDto", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "log", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class BdoApiLogDto : IBdoDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Name of this instance.
        /// </summary>
        [JsonPropertyName("name")]
        [XmlAttribute("name")]
        [DefaultValue(null)]
        public string Name { get; set; }

        /// <summary>
        /// The display name of this instance.
        /// </summary>
        [JsonPropertyName("displayName")]
        [XmlElement("displayName")]
        [DefaultValue("")]
        public string DisplayName { get; set; }

        /// <summary>
        /// The description of this instance.
        /// </summary>
        [JsonPropertyName("description")]
        [XmlElement("description")]
        [DefaultValue("")]
        public string Description { get; set; }

        /// <summary>
        /// The events of this instance.
        /// </summary>
        [JsonPropertyName("events")]
        [XmlArray("events")]
        [XmlArrayItem("event")]
        public List<BdoApiLogEventDto> Events { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoApiLogDto class.
        /// </summary>
        public BdoApiLogDto() : base()
        {
        }

        #endregion
    }
}
