using BindOpen.System.Data;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.System.Scoping
{
    /// <summary>
    /// This class represents the definition of BindOpen extension item.
    /// </summary>
    [XmlType("ExtensionDefinition", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    public abstract class ExtensionDefinitionDto : IBdoDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The index of this instance.
        /// </summary>
        [JsonPropertyName("index")]
        [XmlAttribute("index")]
        public int Index { get; set; } = -1;

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
        /// Instantiates a new instance of the BdoExtensionDefinitionDto class.
        /// </summary>
        protected ExtensionDefinitionDto()
        {
        }
    }

    #endregion
}

