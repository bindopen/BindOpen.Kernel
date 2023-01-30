using BindOpen.Data.Items;
using BindOpen.Data.Meta;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Configuration
{
    /// <summary>
    /// This class represents a config DTO.
    /// </summary>
    [XmlType("BaseConfiguration", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot("config", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class BdoConfigurationDto : MetaSetDto
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
        /// Name of this instance.
        /// </summary>
        [JsonPropertyName("name")]
        [XmlAttribute("name")]
        [DefaultValue("")]
        public string Name { get; set; }

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
        public BdoConfigurationDto() : base()
        {
        }

        #endregion
    }
}
