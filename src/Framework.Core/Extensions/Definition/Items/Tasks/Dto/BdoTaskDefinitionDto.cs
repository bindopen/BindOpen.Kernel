using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Data.Specification;
using BindOpen.Framework.Core.Extensions.Runtime.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Definition.Items
{
    /// <summary>
    /// This class represents a task definition.
    /// </summary>
    /// <seealso cref="BdoTaskConfiguration"/>
    [Serializable()]
    [XmlType("BdoTaskDefinition", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "task.definition", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoTaskDefinitionDto : BdoExtensionItemDefinitionDto, IBdoTaskDefinitionDto
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private DataElementSpecSet _inputSpecification = null;
        private DataElementSpecSet _outputSpecification = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Item class of this instance.
        /// </summary>
        [XmlElement("itemClass")]
        public string ItemClass
        {
            get;
            set;
        }

        /// <summary>
        /// Name of the group of this instance.
        /// </summary>
        [XmlElement("groupName")]
        public string GroupName
        {
            get;
            set;
        }

        /// <summary>
        /// Indicates whether this instance is executable.
        /// </summary>
        [XmlElement("isExecutable")]
        public bool IsExecutable
        {
            get;
            set;
        }

        /// <summary>
        /// Maximum index of this instance.
        /// </summary>
        [XmlElement("maximumIndex")]
        [DefaultValue(100)]
        public float MaximumIndex { get; set; } = 100;

        /// <summary>
        /// Input statement of this instance.
        /// </summary>
        /// <seealso cref="OutputSpecification"/>
        [XmlElement("input.specification")]
        public DataElementSpecSet InputSpecification
        {
            get => this._inputSpecification ?? (this._inputSpecification = new DataElementSpecSet());
            set
            {
                this._inputSpecification = value;
            }
        }

        /// <summary>
        /// Output statement of this instance.
        /// </summary>
        /// <seealso cref="InputSpecification"/>
        [XmlElement("output.specification")]
        public DataElementSpecSet OutputSpecification
        {
            get => this._outputSpecification ?? (this._outputSpecification = new DataElementSpecSet());
            set
            {
                this._outputSpecification = value;
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
        public BdoTaskDefinitionDto() : this(null, "Task_", null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TaskDefinition class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="preffix">The preffix to consider.</param>
        /// <param name="id">The ID to consider.</param>
        public BdoTaskDefinitionDto(string name, string preffix, string id) : base(name, preffix, id)
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
        public List<IDataElementSpec> GetEntries(params TaskEntryKind[] taskEntryKinds)
        {
            if (taskEntryKinds.Length == 0)
                taskEntryKinds = new TaskEntryKind[1] { TaskEntryKind.Any };

            List<IDataElementSpec> dataElements = new List<IDataElementSpec>();

            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.Input)))
                dataElements.AddRange(this._inputSpecification.Items);
            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.Output)))
                dataElements.AddRange(this._outputSpecification.Items);
            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.ScalarOutput)))
                dataElements.AddRange(this._outputSpecification.Items.Where(p => p.ValueType.IsScalar()));
            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.ScalarOutput)))
                dataElements.AddRange(this._outputSpecification.Items.Where(p => p.ValueType.IsScalar()));

            return dataElements;
        }

        /// <summary>
        /// Returns the entry of the specified kind with the specified unique name.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="taskEntryKinds">The kind end entries to consider.</param>
        /// <returns>Returns the input with the specified name.</returns>
        public IDataElementSpec GetEntryWithName(String key, params TaskEntryKind[] taskEntryKinds)
        {
            return this.GetEntries(taskEntryKinds).Find(p => p.KeyEquals(key));
        }

        /// <summary>
        /// Gets the value of the specified entry.
        /// </summary>
        /// <param name="name">The name of the entry to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <param name="taskEntryKinds">The kind end entries to consider.</param>
        public object GetEntryDefaultItemWithName(
            string name,
            IBdoLog log = null,
            params TaskEntryKind[] taskEntryKinds)
        {
            IDataElementSpec entry = this.GetEntryWithName(name, taskEntryKinds);

            return entry?.GetDefaultItemObject(log);
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
        public virtual IBdoLog CustomCheck()
        {
            return new BdoLog();
        }

        /// <summary>
        /// Repairs this instance basing on the specified definition task.
        /// </summary>
        /// <param name="taskDefinition">The definition task to consider.</param>
        public void Repair(IBdoTaskDefinitionDto taskDefinition)
        {
            if (taskDefinition != null)
            {
                if (taskDefinition.Title != null)
                    this.Title = taskDefinition.Title.Clone() as DictionaryDataItem;
                if (taskDefinition.Description != null)
                    this.Description = taskDefinition.Description.Clone() as DictionaryDataItem;

                this._inputSpecification.Repair(
                    taskDefinition.InputSpecification,
                    DataElementSpec.__Arenames.ToList().Excluding(new[] { nameof(DataAreaKind.Items) }).ToArray());
                this._outputSpecification.Repair(
                    taskDefinition.OutputSpecification,
                    DataElementSpec.__Arenames.ToList().Excluding(new[] { nameof(DataAreaKind.Items) }).ToArray());
            }
        }
        #endregion
    }
}