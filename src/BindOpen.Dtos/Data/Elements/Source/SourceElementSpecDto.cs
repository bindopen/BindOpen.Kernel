using BindOpen.Meta.Items;
using BindOpen.Meta.Specification;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Meta.Elements
{
    /// <summary>
    /// This class represents a data source element specification.
    /// </summary>
    [XmlType("SourceElementSpec", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "specification", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
    public class SourceElementSpecDto : BdoElementSpecDto
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
        public SourceElementSpecDto()
        {
        }

        #endregion
    }
}
