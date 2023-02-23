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
    public abstract class ExtensionConfigurationDto : ConfigurationDto
    {
        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        [JsonPropertyName("extensionKind")]
        [XmlAttribute("extensionKind")]
        public BdoExtensionKind ExtensionKind { get; } = BdoExtensionKind.Any;

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
        /// Instantiates a new instance of the TBdoExtensionConfigurationDto class.
        /// </summary>
        protected ExtensionConfigurationDto()
        {
        }

        #endregion
    }
}
