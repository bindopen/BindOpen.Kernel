using BindOpen.Framework.Core.Extensions.Definition.Tasks;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Indexes
{
    /// <summary>
    /// This class represents a task index.
    /// </summary>
    [Serializable()]
    [XmlType("TaskIndex", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "tasks.index", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class TaskIndex : TAppExtensionItemIndex<TaskDefinition>
    {

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TaskIndex class.
        /// </summary>
        public TaskIndex()
        {
        }

        #endregion


    }
}
