using BindOpen.System.Data.Assemblies;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a config DTO.
    /// </summary>
    [XmlType("Configuration", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot("config", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class ConfigurationDto : MetaSetDto
    {
        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        /// <summary>
        /// The definition of this instance.
        /// </summary>
        [JsonPropertyName("definition")]
        [XmlElement("definition")]
        public DefinitionReferenceDto Definition { get; set; }

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
        public TextDictionaryDto Description { get; set; }

        /// <summary>
        /// The description DTO of this instance.
        /// </summary>
        [JsonPropertyName("title")]
        [XmlElement("title")]
        public TextDictionaryDto Title { get; set; }

        /// <summary>
        /// The children of this instance.
        /// </summary>
        [JsonPropertyName("children")]
        [XmlElement("children")]
        public List<ConfigurationDto> Children { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore()]
        [XmlIgnore()]
        public bool ChildrenSpecficied => Children?.Count > 0;
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

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore()]
        [XmlIgnore()]
        public bool ShouldUsedItemIds => UsedItemIds?.Count > 0;

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
