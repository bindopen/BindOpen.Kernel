using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Items.Tasks.Definition;
using BindOpen.Framework.Core.Extensions.Items.Tasks.Definition.Dto;

namespace BindOpen.Framework.Core.Extensions.Indexes.Tasks
{
    /// <summary>
    /// This class represents a task index.
    /// </summary>
    [Serializable()]
    [XmlType("TaskIndex", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "tasks.index", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class TaskIndexDto : TAppExtensionItemIndexDto<TaskDefinitionDto>
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TaskIndex class.
        /// </summary>
        public TaskIndexDto()
        {
        }

        #endregion
    }
}
