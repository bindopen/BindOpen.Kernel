using BindOpen.Data.Configuration;
using BindOpen.Extensions;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Dtos.Extensions
{
    /// <summary>
    /// This class represents a BindOpen extension item configuration.
    /// </summary>
    public abstract class ExtensionItemConfigurationDto : ConfigurationDto
    {
        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        [JsonPropertyName("kind")]
        [XmlAttribute("kind")]
        public BdoExtensionItemKind Kind { get; } = BdoExtensionItemKind.Any;

        /// <summary>
        /// Name of this instance.
        /// </summary>
        [JsonPropertyName("definitionUniqueName")]
        [XmlElement("definitionUniqueName")]
        public string DefinitionUniqueName { get; set; }

        /// <summary>
        /// The group of this instance.
        /// </summary>
        [JsonPropertyName("group")]
        [XmlElement("group")]
        [DefaultValue("")]
        public string GroupId { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TBdoExtensionItemConfigurationDto class.
        /// </summary>
        protected ExtensionItemConfigurationDto()
        {
        }

        #endregion
    }
}
