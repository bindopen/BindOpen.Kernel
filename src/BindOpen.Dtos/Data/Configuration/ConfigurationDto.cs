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
    public class ConfigurationDto : MetaListDto
    {
        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        [JsonPropertyName("creationDate")]
        [XmlElement("creationDate")]
        public string CreationDate { get; set; }

        /// <summary>
        /// The description DTO of this instance.
        /// </summary>
        [JsonPropertyName("description")]
        [XmlElement("description")]
        public DictionaryDto Description { get; set; }

        /// <summary>
        /// Last modification date of this instance.
        /// </summary>
        [JsonPropertyName("lastModificationDate")]
        [XmlElement("lastModificationDate")]
        public string LastModificationDate { get; set; }

        /// <summary>
        /// The using file paths of this instance.
        /// </summary>
        [JsonPropertyName("usingFilePaths")]
        [XmlArray("usingFilePaths")]
        [XmlArrayItem("add")]
        public List<string> UsingFilePaths { get; set; }

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
