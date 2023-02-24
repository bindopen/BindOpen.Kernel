using BindOpen.Data.Items;
using BindOpen.Data.Meta;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Configuration
{
    /// <summary>
    /// This class represents a config DTO.
    /// </summary>
    [XmlType("Configuration", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot("config", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class ConfigurationDto : MetaSetDto
    {
        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        [JsonPropertyName("definitionUniqueName")]
        [XmlElement("definitionUniqueName")]
        public string DefinitionUniqueName { get; set; }

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        [JsonPropertyName("creationDate")]
        [XmlElement("creationDate")]
        public string CreationDate { get; set; }

        /// <summary>
        /// Last modification date of this instance.
        /// </summary>
        [JsonPropertyName("lastModificationDate")]
        [XmlElement("lastModificationDate")]
        public string LastModificationDate { get; set; }

        /// <summary>
        /// The description DTO of this instance.
        /// </summary>
        [JsonPropertyName("description")]
        [XmlElement("description")]
        public DictionaryDto Description { get; set; }

        /// <summary>
        /// The description DTO of this instance.
        /// </summary>
        [JsonPropertyName("title")]
        [XmlElement("title")]
        public DictionaryDto Title { get; set; }

        /// <summary>
        /// The using item IDs of this instance.
        /// </summary>
        [JsonPropertyName("usedItemIds")]
        [XmlArray("usedItemIds")]
        [XmlArrayItem("add")]
        public List<string> UsedItemIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore()]
        [XmlIgnore()]
        public bool UsedItemIdsSpecified => UsedItemIds?.Count > 0;

        #endregion

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfigurationDto class.
        /// </summary>
        public ConfigurationDto() : base()
        {
        }

        #endregion
    }
}
