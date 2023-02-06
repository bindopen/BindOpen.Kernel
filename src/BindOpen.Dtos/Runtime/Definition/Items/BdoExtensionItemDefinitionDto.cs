using BindOpen.Data;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents the definition of BindOpen extension item.
    /// </summary>
    [XmlType("ExtensionItemDefinition", Namespace = "https://xsd.bindopen.org")]
    public abstract class BdoExtensionItemDefinitionDto : IndexedDataItemDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Uri of the image representing this instance.
        /// </summary>
        [JsonPropertyName("imageUri")]
        [XmlElement("imageUri")]
        public string ImageUri { get; set; }

        /// <summary>
        /// Indicates whether this instance is editable.
        /// </summary>
        [JsonPropertyName("isEditable")]
        [XmlElement("isEditable")]
        [DefaultValue(true)]
        public bool IsEditable { get; set; } = true;

        /// <summary>
        /// Indicates whether this instance is indexed.
        /// </summary>
        [JsonPropertyName("isIndexed")]
        [XmlElement("isIndexed")]
        [DefaultValue(true)]
        public bool IsIndexed { get; set; } = true;

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoExtensionItemDefinitionDto class.
        /// </summary>
        protected BdoExtensionItemDefinitionDto()
        {
        }
    }

    #endregion
}

