using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Definition.Tasks;

namespace BindOpen.Framework.Core.Extensions.Indexes.Tasks
{
    /// <summary>
    /// This class represents a task index.
    /// </summary>
    [Serializable()]
    [XmlType("TaskIndex", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "tasks.index", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class TaskIndexDto : TAppExtensionItemIndexDto<ITaskDefinitionDto>
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
