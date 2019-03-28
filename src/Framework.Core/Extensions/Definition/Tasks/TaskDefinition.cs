using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Data.Specification;
using BindOpen.Framework.Core.Extensions.Configuration.Tasks;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

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

        private DataElementSpecSet _InputSpecification = null;
        private DataElementSpecSet _OutputSpecification = null;

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
        public DataElementSpecSet InputSpecification
        {
            get
            {
                if (this._InputSpecification == null) this._InputSpecification = new DataElementSpecSet();
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
        public DataElementSpecSet OutputSpecification
        {
            get
            {
                if (this._OutputSpecification == null) this._OutputSpecification = new DataElementSpecSet();
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
        public TaskDefinition() : this(null, "Task_", null)
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

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        // Entries --------------------------------

        /// <summary>
        /// Gets the specified entries.
        /// </summary>
        /// <param name="taskEntryKinds">The kind end entries to consider.</param>
        /// <returns>True if this instance is configurable.</returns>
        public List<DataElementSpec> GetEntries(params TaskEntryKind[] taskEntryKinds)
        {
            if (taskEntryKinds.Length == 0)
                taskEntryKinds = new TaskEntryKind[1] { TaskEntryKind.Any };

            List<DataElementSpec> dataElements = new List<DataElementSpec>();
            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.Input)))
                dataElements.AddRange(this._InputSpecification.Items);
            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.Output)))
                dataElements.AddRange(this._OutputSpecification.Items);
            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.ScalarOutput)))
                dataElements.AddRange(this._OutputSpecification.Items.Where(p => p.ValueType.IsScalar()));
            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.ScalarOutput)))
                dataElements.AddRange(this._OutputSpecification.Items.Where(p => p.ValueType.IsScalar()));

            return dataElements;
        }

        /// <summary>
        /// Returns the entry of the specified kind with the specified unique name.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="taskEntryKinds">The kind end entries to consider.</param>
        /// <returns>Returns the input with the specified name.</returns>
        public DataElementSpec GetEntryWithName(String key, params TaskEntryKind[] taskEntryKinds)
        {
            return this.GetEntries(taskEntryKinds).Find(p => p.KeyEquals(key));
        }

        /// <summary>
        /// Gets the value of the specified entry.
        /// </summary>
        /// <param name="name">The name of the entry to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <param name="taskEntryKinds">The kind end entries to consider.</param>
        public Object GetEntryDefaultValueWithName(
            String name,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null,
            params TaskEntryKind[] taskEntryKinds)
        {
            DataElementSpec entry = this.GetEntryWithName(name, taskEntryKinds);

            return entry?.GetItemObject(appScope, scriptVariableSet, log);
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
                    DataElementSpec.__Arenames.Excluding(DataAreaKind.Items.ToString()));
                this._OutputSpecification.Repair(
                    taskDefinition.OutputSpecification,
                    DataElementSpec.__Arenames.Excluding(DataAreaKind.Items.ToString()));
            }
        }


        #endregion
    }
}