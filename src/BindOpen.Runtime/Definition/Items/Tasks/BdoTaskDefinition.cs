using BindOpen.Logging;
using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents a task definition.
    /// </summary>
    /// <seealso cref="BdoTaskConfiguration"/>
    public class BdoTaskDefinition : BdoExtensionItemDefinition, IBdoTaskDefinition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Name of the group of this instance.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Input specification of this instance.
        /// </summary>
        public IBdoMetaSpecSet InputSpecification { get; set; }

        /// <summary>
        /// Indicates whether this instance is executable.
        /// </summary>
        public bool IsExecutable { get; set; } = true;

        /// <summary>
        /// Item class of this instance.
        /// </summary>
        public string ItemClass { get; set; }

        /// <summary>
        /// Maximum index of this instance.
        /// </summary>
        public float MaximumIndex { get; set; } = 100;

        /// <summary>
        /// Output specification of this instance.
        /// </summary>
        public IBdoMetaSpecSet OutputSpecification { get; set; }

        /// <summary>
        /// The runtime type of this instance.
        /// </summary>
        public Type RuntimeType { get; set; }

        /// <summary>
        /// The unique ID of this instance.
        /// </summary>
        public new string UniqueId { get => ExtensionDefinition?.UniqueId + "$" + Name; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TaskDefinition class. 
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="extensionDefinition">The extensition definition to consider.</param>
        public BdoTaskDefinition(
            string name,
            IBdoExtensionDefinition extensionDefinition) : base(name, "task_", extensionDefinition)
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the key of this instance.
        /// </summary>
        /// <returns></returns>
        public override string Key()
        {
            return UniqueId;
        }

        // Entries --------------------------------

        /// <summary>
        /// Gets the specified entries.
        /// </summary>
        /// <param name="taskEntryKinds">The kind end entries to consider.</param>
        /// <returns>True if this instance is configurable.</returns>
        public List<IBdoMetaDataSpec> GetEntries(params TaskEntryKind[] taskEntryKinds)
        {
            if (taskEntryKinds.Length == 0)
                taskEntryKinds = new TaskEntryKind[1] { TaskEntryKind.Any };

            var dataElements = new List<IBdoMetaDataSpec>();

            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.Input)))
                dataElements.AddRange(InputSpecification.Items);
            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.Output)))
                dataElements.AddRange(OutputSpecification.Items);
            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.ScalarOutput)))
                dataElements.AddRange(OutputSpecification.Items.Where(p => p.ValueType.IsScalar()));
            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.ScalarOutput)))
                dataElements.AddRange(OutputSpecification.Items.Where(p => p.ValueType.IsScalar()));

            return dataElements;
        }

        /// <summary>
        /// Returns the entry of the specified kind with the specified unique name.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="taskEntryKinds">The kind end entries to consider.</param>
        /// <returns>Returns the input with the specified name.</returns>
        public IBdoMetaDataSpec GetEntryWithName(string key, params TaskEntryKind[] taskEntryKinds)
        {
            return GetEntries(taskEntryKinds).Find(p => p.BdoKeyEquals(key));
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
            IBdoMetaDataSpec entry = GetEntryWithName(name, taskEntryKinds);

            return entry?.DefaultItem;
        }

        #endregion

        // --------------------------------------------------
        // CHECK
        // --------------------------------------------------

        #region Check

        /// <summary>
        /// Repairs this instance basing on the specified definition task.
        /// </summary>
        /// <param name="taskDefinition">The definition task to consider.</param>
        public void Repair(IBdoTaskDefinition taskDefinition)
        {
            if (taskDefinition != null)
            {
                if (taskDefinition.Title != null)
                    Title = taskDefinition.Title.Clone() as BdoDictionary;
                if (taskDefinition.Description != null)
                    Description = taskDefinition.Description.Clone() as BdoDictionary;

                //InputSpecification.Repair(
                //    taskDefinition.InputSpecification,
                //    BdoElementSpec.__Arenames.ToList().Excluding(new[] { nameof(DataAreaKind.Items) }).ToArray());
                //OutputSpecification.Repair(
                //    taskDefinition.OutputSpecification,
                //    BdoElementSpec.__Arenames.ToList().Excluding(new[] { nameof(DataAreaKind.Items) }).ToArray());
            }
        }
        #endregion

        // ------------------------------------------
        // IDISPOSABLE METHODS
        // ------------------------------------------

        #region IDisposable_Methods

        private bool _isDisposed = false;

        /// <summary>
        /// Disposes this instance. 
        /// </summary>
        /// <param name="isDisposing">Indicates whether this instance is disposing</param>
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            InputSpecification?.Dispose();
            OutputSpecification?.Dispose();

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }
}