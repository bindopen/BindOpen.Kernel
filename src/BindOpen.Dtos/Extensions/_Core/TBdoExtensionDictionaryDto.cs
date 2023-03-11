using BindOpen.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This class represents a BindOpen extension dico.
    /// </summary>
    /// <typeparam name="T">The class of extension item definition to consider.</typeparam>
    public class TBdoExtensionDictionaryDto<T> : IDto
        where T : ExtensionDefinitionDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Creation date of this instance.
        /// </summary>
        [JsonPropertyName("creationDate")]
        [XmlElement("creationDate")]
        [DefaultValue("")]
        public string CreationDate { get; set; }

        /// <summary>
        /// Last modification date of this instance.
        /// </summary>
        [JsonPropertyName("lastModificationDate")]
        [XmlElement("lastModificationDate")]
        [DefaultValue("")]
        public string LastModificationDate { get; set; }

        /// <summary>
        /// ID of the library of this instance.
        /// </summary>
        [JsonPropertyName("libraryId")]
        [XmlAttribute("libraryId")]
        public string LibraryId { get; set; }

        /// <summary>
        /// Name of the library of this instance.
        /// </summary>
        [JsonPropertyName("library")]
        [XmlAttribute("library")]
        public string LibraryName { get; set; }

        /// <summary>
        /// Groups of this instance.
        /// </summary>
        [JsonPropertyName("groups")]
        [XmlArray("groups")]
        [XmlArrayItem("group")]
        public List<ExtensionGroupDto> Groups { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TBdoExtensionDictionaryDto class.
        /// </summary>
        public TBdoExtensionDictionaryDto()
        {
        }

        #endregion
    }
}
