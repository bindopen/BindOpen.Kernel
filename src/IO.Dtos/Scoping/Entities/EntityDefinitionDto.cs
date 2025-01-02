using BindOpen.Data.Assemblies;
using BindOpen.Data.Meta;
using BindOpen.Scoping.Script;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Scoping.Entities
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
        /// The class name of this instance.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public string ItemClassId { get; set; }

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
        [XmlArray("outputs")]
        [XmlArrayItem("node", Type = typeof(MetaNodeDto))]
        [XmlArrayItem("object", Type = typeof(MetaObjectDto))]
        [XmlArrayItem("scalar", Type = typeof(MetaScalarDto))]
        [XmlArrayItem("word", Type = typeof(ScriptwordDto))]
        public List<MetaDataDto> OutputSpecification { get; set; }

        /// <summary>
        /// Indicates whether the OutputSpecification property must be ignored.
        /// </summary>
        [NotMapped]
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
