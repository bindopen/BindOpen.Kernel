using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using BindOpen.Kernel.Scoping;

namespace BindOpen.Kernel.Scoping
{
    /// <summary>
    /// This class represents a DTO task dico.
    /// </summary>
    [XmlType("TaskDictionary", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    [XmlRoot(ElementName = "tasks.dico", Namespace = "https://storage.bindopen.org/xsd/bindopen", IsNullable = false)]
    public class TaskDictionaryDto : TBdoExtensionDictionaryDto<TaskDefinitionDto>
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Definitions of this instance.
        /// </summary>
        [JsonPropertyName("definitions")]
        [XmlArray("definitions")]
        [XmlArrayItem("add.definition")]
        public List<TaskDefinitionDto> Definitions { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoTaskDictionaryDto class.
        /// </summary>
        public TaskDictionaryDto()
        {
        }

        #endregion
    }
}
