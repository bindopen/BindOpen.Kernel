using BindOpen.Data.Meta;
using BindOpen.Scoping.Script;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Scoping.Tasks
{
    /// <summary>
    /// This class represents a task definition.
    /// </summary>
    /// <seealso cref="ConfigurationDto"/>
    [XmlType("TaskDefinition", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel")]
    [XmlRoot(ElementName = "task.definition", Namespace = "https://storage.bindopen.org/xsd/bindopen/kernel", IsNullable = false)]
    public class TaskDefinitionDto : ExtensionDefinitionDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The name of the group of this instance.
        /// </summary>
        [JsonPropertyName("groupName")]
        [XmlElement("groupName")]
        public string GroupName { get; set; }

        /// <summary>
        /// Indicates whether this instance is executable.
        /// </summary>
        [JsonPropertyName("isExecutable")]
        [XmlElement("isExecutable")]
        public bool IsExecutable { get; set; }

        /// <summary>
        /// The item class of this instance.
        /// </summary>
        [JsonPropertyName("itemClass")]
        [XmlElement("itemClass")]
        public string ItemClass { get; set; }

        /// <summary>
        /// The maximum index of this instance.
        /// </summary>
        [JsonPropertyName("maximumIndex")]
        [XmlElement("maximumIndex")]
        [DefaultValue(100)]
        public float MaximumIndex { get; set; } = 100;

        /// <summary>
        /// The inputs of this instance.
        /// </summary>
        [JsonPropertyName("inputs")]
        [XmlArray("inputs")]
        [XmlArrayItem("node", Type = typeof(MetaNodeDto))]
        [XmlArrayItem("object", Type = typeof(MetaObjectDto))]
        [XmlArrayItem("scalar", Type = typeof(MetaScalarDto))]
        [XmlArrayItem("word", Type = typeof(ScriptwordDto))]
        public List<MetaDataDto> InputSpecification { get; set; }

        /// <summary>
        /// Indicates whether the input schema property must be ignored.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        [XmlIgnore]
        public bool InputSpecificationSpecified => InputSpecification?.Count > 0;

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
        /// Indicates whether the output schema property must be ignored.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        [XmlIgnore]
        public bool OutputSpecificationSpecified => OutputSpecification?.Count > 0;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TaskDefinitionDto class. 
        /// </summary>
        public TaskDefinitionDto()
        {
        }

        #endregion
    }
}