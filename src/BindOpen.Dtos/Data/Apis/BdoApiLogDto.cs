using BindOpen.Meta.Items;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Meta.Apis
{
    /// <summary>
    /// This class represents a Api log DTO.
    /// </summary>
    [XmlType("ApiLogDto", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "log", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoApiLogDto : NamedDataItemDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

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
