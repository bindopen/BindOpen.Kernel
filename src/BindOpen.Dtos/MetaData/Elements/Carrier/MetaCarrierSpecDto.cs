using BindOpen.Extensions.Modeling;
using BindOpen.MetaData.Specification;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// This class represents a carrier element specification.
    /// </summary>
    [XmlType("MetaCarrierSpec", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "spec", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class MetaCarrierSpecDto : MetaElementSpecDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The definition filter of this instance.
        /// </summary>
        [JsonPropertyName("definition.filter")]
        [XmlElement("definition.filter")]
        public DataValueFilterDto DefinitionFilter { get; set; }

        /// <summary>
        /// Default items of this instance.
        /// </summary>
        [JsonPropertyName("default.values")]
        [XmlArray("default.values")]
        [XmlArrayItem("add")]
        public List<BdoCarrierConfigurationDto> DefaultItems { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the CarrierElementSpecDto class.
        /// </summary>
        public MetaCarrierSpecDto()
        {
        }

        #endregion
    }

}
