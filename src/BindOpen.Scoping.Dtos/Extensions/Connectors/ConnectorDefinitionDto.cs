using BindOpen.Scoping.Data;
using BindOpen.Scoping.Data.Meta;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Scoping.Extensions.Connectors
{
    /// <summary>
    /// This class represents a DTO connector definition.
    /// </summary>
    [XmlType("ConnectorDefinition", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "connector.definition", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class ConnectorDefinitionDto : ExtensionDefinitionDto
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
        public DatasourceKind DatasourceKind { get; set; } = DatasourceKind.None;

        /// <summary>
        /// Item class of this instance.
        /// </summary>
        [JsonPropertyName("itemClass")]
        [XmlElement("itemClass")]
        public string ItemClass { get; set; }

        // DTO

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("specs")]
        [XmlElement("spec")]
        public List<SpecDto> Specs { get; set; }

        /// <summary>
        /// Indicates whether the entities property must be ignored.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public bool SpecsSpecified => Specs?.Count > 0;
        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ConnectorDefinition class.
        /// </summary>
        public ConnectorDefinitionDto()
        {
        }

        #endregion
    }

}
