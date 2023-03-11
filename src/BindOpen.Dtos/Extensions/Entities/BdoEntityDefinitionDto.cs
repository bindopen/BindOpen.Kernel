using BindOpen.Data.Assemblies;
using BindOpen.Data.Meta;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Entities
{
    /// <summary>
    /// This class represents the entity definition.
    /// </summary>
    [XmlType("EntityDefinition", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "entity.definition", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class BdoEntityDefinitionDto : ExtensionDefinitionDto
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
        public ClassReferenceDto ItemClass { get; set; }

        /// <summary>
        /// Viewer class of this instance.
        /// </summary>
        [JsonPropertyName("viewerClass")]
        [XmlElement("viewerClass")]
        public string ViewerClass { get; set; }

        // DTO

        /// <summary>
        /// The elements of this instance.
        /// </summary>
        [JsonPropertyName("outputs")]
        [XmlElement("output")]
        public List<MetaDataDto> OutputSpecification { get; set; }

        /// <summary>
        /// Indicates whether the entities property must be ignored.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public bool OutputSpecificationSpecified => OutputSpecification?.Count > 0;

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
