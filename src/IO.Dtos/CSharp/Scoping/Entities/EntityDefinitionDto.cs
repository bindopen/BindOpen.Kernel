using BindOpen.Kernel.Data.Assemblies;
using BindOpen.Kernel.Data.Meta;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Kernel.Scoping.Entities
{
    /// <summary>
    /// This class represents the entity definition DTO.
    /// </summary>
    [XmlType("EntityDefinition", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "entity.definition", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class EntityDefinitionDto : ExtensionDefinitionDto
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The item class of this instance.
        /// </summary>
        [JsonPropertyName("itemClass")]
        [XmlElement("itemClass")]
        public ClassReferenceDto ItemClass { get; set; }

        /// <summary>
        /// The viewer class of this instance.
        /// </summary>
        [JsonPropertyName("viewerClass")]
        [XmlElement("viewerClass")]
        public string ViewerClass { get; set; }

        /// <summary>
        /// The outputs of this instance.
        /// </summary>
        [JsonPropertyName("outputs")]
        [XmlElement("output")]
        public List<MetaDataDto> OutputSpecification { get; set; }

        /// <summary>
        /// Indicates whether the OutputSpecification property must be ignored.
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
        /// Instantiates a new instance of the EntityDefinitionDto class.
        /// </summary>
        public EntityDefinitionDto()
        {
        }

        #endregion
    }
}
