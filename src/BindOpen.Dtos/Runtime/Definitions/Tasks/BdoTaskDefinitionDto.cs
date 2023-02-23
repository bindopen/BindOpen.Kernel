using BindOpen.Data.Meta;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Runtime.Definitions
{
    /// <summary>
    /// This class represents a task definition.
    /// </summary>
    /// <seealso cref="BdoConfig"/>
    [XmlType("TaskDefinition", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "task.definition", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class BdoTaskDefinitionDto : ExtensionDefinitionDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Name of the group of this instance.
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
        /// Item class of this instance.
        /// </summary>
        [JsonPropertyName("itemClass")]
        [XmlElement("itemClass")]
        public string ItemClass { get; set; }

        /// <summary>
        /// Maximum index of this instance.
        /// </summary>
        [JsonPropertyName("maximumIndex")]
        [XmlElement("maximumIndex")]
        [DefaultValue(100)]
        public float MaximumIndex { get; set; } = 100;

        // DTO

        /// <summary>
        /// Input specification of this instance.
        /// </summary>
        /// <seealso cref="OutputSpecification"/>
        [JsonPropertyName("input.specification")]
        [XmlElement("input.specification")]
        public SpecListDto InputSpecification { get; set; }

        /// <summary>
        /// Output specification of this instance.
        /// </summary>
        /// <seealso cref="InputSpecification"/>
        [JsonPropertyName("output.specification")]
        [XmlElement("output.specification")]
        public SpecListDto OutputSpecification { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TaskDefinition class. 
        /// </summary>
        public BdoTaskDefinitionDto()
        {
        }

        #endregion
    }
}