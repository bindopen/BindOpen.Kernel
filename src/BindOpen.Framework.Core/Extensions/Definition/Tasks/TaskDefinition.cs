using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Data.Specification;
using BindOpen.Framework.Core.Extensions.Configuration.Tasks;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Definition.Tasks
{
    /// <summary>
    /// This class represents a task definition.
    /// </summary>
    /// <seealso cref="TaskConfiguration"/>
    [Serializable()]
    [XmlType("TaskDefinition", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "task.definition", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class TaskDefinition : AppExtensionItemDefinition
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private DataElementSpecificationSet _InputSpecification = null;
        private DataElementSpecificationSet _OutputSpecification = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Item class of this instance.
        /// </summary>
        [XmlElement("itemClass")]
        public String ItemClass
        {
            get;
            set;
        }

        /// <summary>
        /// Name of the group of this instance.
        /// </summary>
        [XmlElement("groupName")]
        public String GroupName
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates whether this instance is executable.
        /// </summary>
        [XmlElement("isExecutable")]
        public Boolean IsExecutable
        {
            get;
            set;
        }

        /// <summary>
        /// Input statement of this instance.
        /// </summary>
        /// <seealso cref="OutputSpecification"/>
        [XmlElement("input.specification")]
        public DataElementSpecificationSet InputSpecification
        {
            get
            {
                if (this._InputSpecification == null) this._InputSpecification = new DataElementSpecificationSet();
                return this._InputSpecification;
            }
            set
            {
                this._InputSpecification = value;
            }
        }

        /// <summary>
        /// Output statement of this instance.
        /// </summary>
        /// <seealso cref="InputSpecification"/>
        [XmlElement("output.specification")]
        public DataElementSpecificationSet OutputSpecification
        {
            get
            {
                if (this._OutputSpecification == null) this._OutputSpecification = new DataElementSpecificationSet();
                return this._OutputSpecification;
            }
            set
            {
                this._OutputSpecification = value;
            }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TaskDefinition class. 
        /// </summary>
        public TaskDefinition(): this(null, "Task_", null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TaskDefinition class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="preffix">The preffix to consider.</param>
        /// <param name="id">The ID to consider.</param>
        public TaskDefinition(string name, string preffix, string id) : base(name, preffix, id)
        {
            this.IsIndexed = false;
            this.IsExecutable = false;
        }

        #endregion

        // --------------------------------------------------
        // CHECK
        // --------------------------------------------------

        #region Check

        /// <summary>
        /// Checks this instance in a custom way.
        /// </summary>
        /// <returns>Returns the check log.</returns>
        public virtual Log CustomCheck()
        {
            return new Log();
        }

        /// <summary>
        /// Repairs this instance basing on the specified definition task.
        /// </summary>
        /// <param name="taskDefinition">The definition task to consider.</param>
        public void Repair(TaskDefinition taskDefinition)
        {
            if (taskDefinition != null)
            {
                if (!String.IsNullOrEmpty(taskDefinition.Key()))
                {
                    if (this.UniqueId == null || this.KeyEquals(taskDefinition))
                    {
                        this.UniqueId = taskDefinition.UniqueId;
                    }
                }

                if (taskDefinition.Title != null)
                    this.Title = taskDefinition.Title.Clone() as DictionaryDataItem;
                if (taskDefinition.Description != null)
                    this.Description = taskDefinition.Description.Clone() as DictionaryDataItem;

                this._InputSpecification.Repair(
                    taskDefinition.InputSpecification,
                    DataElementSpecification.__Arenames.Excluding(DataAreaKind.Items.ToString()));
                this._OutputSpecification.Repair(
                    taskDefinition.OutputSpecification,
                    DataElementSpecification.__Arenames.Excluding(DataAreaKind.Items.ToString()));
            }
        }


        #endregion
    }
}