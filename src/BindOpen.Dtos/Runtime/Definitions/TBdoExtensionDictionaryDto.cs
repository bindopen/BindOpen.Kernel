using BindOpen.Data;
using BindOpen.Runtime.Definitions;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Definitions
{
    /// <summary>
    /// This class represents a BindOpen extension dico.
    /// </summary>
    /// <typeparam name="T">The class of extension item definition to consider.</typeparam>
    public class TBdoExtensionDictionaryDto<T> : StoredDataItemDto
        where T : ExtensionDefinitionDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

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
