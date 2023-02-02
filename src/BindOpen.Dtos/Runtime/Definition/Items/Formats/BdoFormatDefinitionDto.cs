using BindOpen.Data;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents the format definition.
    /// </summary>
    [XmlType("FormatDefinition", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "format.definition", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class BdoFormatDefinitionDto : BdoExtensionItemDefinitionDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Data source kind of this instance.
        /// </summary>
        [JsonPropertyName("dataSourceKind")]
        [XmlElement("dataSourceKind")]
        public DatasourceKind DatasourceKind { get; set; } = DatasourceKind.Memory;

        /// <summary>
        /// Item class of this instance.
        /// </summary>
        [JsonPropertyName("itemClass")]
        [XmlElement("itemClass")]
        public string ItemClass { get; set; }

        /// <summary>
        /// Viewer class of this instance.
        /// </summary>
        /// <remarks>Class using the following format: winForm=xxx.xxx.xxx;webForm=xxx.xxx.xxx</remarks>
        [JsonPropertyName("viewerClass")]
        [XmlElement("viewerClass")]
        public string ViewerClass { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the FormatDefinition class.
        /// </summary>
        public BdoFormatDefinitionDto()
        {
        }

        #endregion
    }
}
