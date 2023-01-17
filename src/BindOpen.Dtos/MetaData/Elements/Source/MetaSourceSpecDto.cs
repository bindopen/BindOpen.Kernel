using BindOpen.MetaData.Items;
using BindOpen.MetaData.Specification;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// This class represents a data source element specification.
    /// </summary>
    [XmlType("MetaSourceSpec", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "spec", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class MetaSourceSpecDto : MetaElementSpecDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The data source kind of this instance.
        /// </summary>
        [JsonPropertyName("kind")]
        [XmlAttribute("kind")]
        public DatasourceKind DatasourceKind { get; set; } = DatasourceKind.Any;

        /// <summary>
        /// The definition filter of this instance.
        /// </summary>
        [JsonPropertyName("definition.filter")]
        [XmlElement("definition.filter")]
        public DataValueFilterDto DefinitionFilter { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SourceElementSpecDto class.
        /// </summary>
        public MetaSourceSpecDto()
        {
        }

        #endregion
    }
}
