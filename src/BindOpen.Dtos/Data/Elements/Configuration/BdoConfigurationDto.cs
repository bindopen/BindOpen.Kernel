using BindOpen.Data.Items;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This class represents a configuration DTO.
    /// </summary>
    [XmlType("BaseConfiguration", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot("config", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoConfigurationDto : BdoElementSetDto
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
        public DictionaryDataItemDto Description { get; set; }

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
