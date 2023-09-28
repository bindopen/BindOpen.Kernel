using BindOpen.Kernel.Data.Meta;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Kernel.Scoping.Tasks
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
        [XmlElement("input")]
        public List<MetaDataDto> InputSpecification { get; set; }

        /// <summary>
        /// Indicates whether the input specification property must be ignored.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public bool InputSpecificationSpecified => InputSpecification?.Count > 0;

        /// <summary>
        /// The outputs of this instance.
        /// </summary>
        [JsonPropertyName("outputs")]
        [XmlElement("output")]
        public List<MetaDataDto> OutputSpecification { get; set; }

        /// <summary>
        /// Indicates whether the output specification property must be ignored.
        /// </summary>
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