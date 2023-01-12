using BindOpen.Meta.Elements;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents the entity definition.
    /// </summary>
    [XmlType("EntityDefinition", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "entity.definition", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class BdoEntityDefinitionDto : BdoExtensionItemDefinitionDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Item class of this instance.
        /// </summary>
        [JsonPropertyName("itemClass")]
        [XmlElement("itemClass")]
        public string ItemClass { get; set; }

        /// <summary>
        /// The kind of this instance. 
        /// </summary>
        [JsonPropertyName("kind")]
        [XmlElement("kind")]
        public BdoEntityKind Kind { get; set; } = BdoEntityKind.Any;

        /// <summary>
        /// Viewer class of this instance.
        /// </summary>
        [JsonPropertyName("viewerClass")]
        [XmlElement("viewerClass")]
        public string ViewerClass { get; set; }

        // DTO

        /// <summary>
        /// The set of detail specifications of this instance.
        /// </summary>
        [JsonPropertyName("detail.specification")]
        [XmlElement("detail.specification")]
        public BdoElementSpecSetDto DetailSpec { get; set; }

        /// <summary>
        /// Formats of this instance.
        /// </summary>
        [JsonPropertyName("formats")]
        [XmlArray("formats")]
        [XmlArrayItem("format")]
        public List<BdoFormatDefinitionDto> FormatDefinitions { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the EntityDefinition class.
        /// </summary>
        public BdoEntityDefinitionDto()
        {
        }

        #endregion
    }
}
