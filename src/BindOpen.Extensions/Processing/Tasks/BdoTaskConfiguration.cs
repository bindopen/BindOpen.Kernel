using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Runtime.Definition;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Extensions.Processing
{
    /// <summary>
    /// This class represents a task configuration.
    /// </summary>
    public class BdoTaskConfiguration
        : TBdoExtensionItemConfiguration<IBdoTaskDefinition>,
        IBdoTaskConfiguration
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoTaskConfiguration class.
        /// </summary>
        public BdoTaskConfiguration() : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoTaskConfiguration class.
        /// </summary>
        public BdoTaskConfiguration(string definitionUniqueId)
            : base(BdoExtensionItemKind.Task, definitionUniqueId)
        {
        }

        #endregion

        // ------------------------------------------
        // IBdoTaskConfiguration Implementation
        // ------------------------------------------

        #region IBdoTaskConfiguration

        // Properties

        /// <summary>
        /// Input detail of this instance.
        /// </summary>
        public IBdoMetaSet InputDetail { get; set; }

        /// <summary>
        /// Output detail of this instance.
        /// </summary>
        public IBdoMetaSet OutputDetail { get; set; }

        // Methods

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="items">The items of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public new IBdoTaskConfiguration Add(params IBdoMetaData[] items)
            => base.Add(items) as IBdoTaskConfiguration;

        /// <summary>
        /// Sets the specified single item of this instance.
        /// </summary>
        /// <param name="items">The items to apply to this instance.</param>
        /// <remarks>Items of this instance must be allowed and must not be forbidden. Otherwise, the values will be the default ones..</remarks>
        public new IBdoTaskConfiguration WithItems(params IBdoMetaData[] items)
            => base.WithItems(items) as IBdoTaskConfiguration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputs"></param>
        public IBdoTaskConfiguration AddInputs(params IBdoMetaData[] inputs)
        {
            InputDetail ??= new BdoMetaSet();
            InputDetail.Add(inputs);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputs"></param>
        public IBdoTaskConfiguration WithInputs(params IBdoMetaData[] inputs)
        {
            InputDetail ??= new BdoMetaSet();
            InputDetail.WithItems(inputs);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outputs"></param>
        public IBdoTaskConfiguration AddOutputs(params IBdoMetaData[] outputs)
        {
            OutputDetail ??= new BdoMetaSet();
            OutputDetail.Add(outputs);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outputs"></param>
        public IBdoTaskConfiguration WithOutputs(params IBdoMetaData[] outputs)
        {
            OutputDetail ??= new BdoMetaSet();
            OutputDetail.WithItems(outputs);
            return this;
        }

        // Accessors --------------------------------

        /// <summary>
        /// Gets the specified entries.
        /// </summary>
        /// <param name="taskEntryKinds">The kind end entries to consider.</param>
        /// <returns>True if this instance is configurable.</returns>
        public List<IBdoMetaData> GetEntries(params TaskEntryKind[] taskEntryKinds)
        {
            if (taskEntryKinds.Length == 0)
                taskEntryKinds = new TaskEntryKind[1] { TaskEntryKind.Any };

            var elements = new List<IBdoMetaData>();

            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.Input)))
            {
                elements.AddRange(InputDetail.ToList());
            }

            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.Output)))
                elements.AddRange(OutputDetail.Items);
            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.ScalarOutput)))
                elements.AddRange(OutputDetail.Items.Where(p => p.ValueType.IsScalar()));
            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.NonScalarOutput)))
                elements.AddRange(OutputDetail.Items.Where(p => !p.ValueType.IsScalar()));

            return elements.Distinct().ToList();
        }

        /// <summary>
        /// Returns the entry of the specified kind with the specified unique name.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="taskEntryKinds">The kind end entries to consider.</param>
        /// <returns>Returns the input with the specified name.</returns>
        public IBdoMetaData GetEntryWithName(string key, params TaskEntryKind[] taskEntryKinds)
        {
            return GetEntries(taskEntryKinds).Find(p => p.BdoKeyEquals(key));
        }

        #endregion

        //------------------------------------------
        // CLONING
        //-----------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned metrics definition.</returns>
        public override object Clone(params string[] areas)
        {
            BdoTaskConfiguration task = base.Clone(areas) as BdoTaskConfiguration;

            if (InputDetail != null)
            {
                task.InputDetail = InputDetail.Clone() as BdoMetaSet;
            }

            if (OutputDetail != null)
            {
                task.OutputDetail = OutputDetail.Clone() as BdoMetaSet;
            }

            return task;
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

            InputDetail?.Dispose();
            OutputDetail?.Dispose();

            _isDisposed = true;

            base.Dispose(isDisposing);
        }

        #endregion
    }
}