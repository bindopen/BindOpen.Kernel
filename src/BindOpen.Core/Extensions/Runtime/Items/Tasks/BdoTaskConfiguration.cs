using BindOpen.Application.Scopes;
using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Extensions.Definition;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// This class represents a task configuration.
    /// </summary>
    [XmlType("BdoTaskConfiguration", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "task", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class BdoTaskConfiguration
        : TBdoExtensionTitledItemConfiguration<BdoTaskDefinition>, IBdoTaskConfiguration
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        // Entries --------------------------------

        private DataElementSet _inputDetail = new DataElementSet();

        private DataElementSet _outputDetail = new DataElementSet();

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Input detail of this instance.
        /// </summary>
        [XmlElement("inputs")]
        public DataElementSet InputDetail
        {
            get => _inputDetail ?? (_inputDetail = new DataElementSet());
            set => _inputDetail = value;
        }

        /// <summary>
        /// Output detail of this instance.
        /// </summary>
        [XmlElement("outputs")]
        public DataElementSet OutputDetail
        {
            get => _outputDetail ?? (_outputDetail = new DataElementSet());
            set => _outputDetail = value;
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TaskConfiguration class.
        /// </summary>
        public BdoTaskConfiguration() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TaskConfiguration class.
        /// </summary>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        /// <param name="items">The items to consider.</param>
        public BdoTaskConfiguration(
            string definitionUniqueId,
            params DataElement[] items)
            : base(BdoExtensionItemKind.Task, definitionUniqueId, items)
        {
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
        public List<IDataElement> GetEntries(params TaskEntryKind[] taskEntryKinds)
        {
            if (taskEntryKinds.Length == 0)
                taskEntryKinds = new TaskEntryKind[1] { TaskEntryKind.Any };

            List<IDataElement> dataElements = new List<IDataElement>();

            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.Input)))
                dataElements.AddRange(_inputDetail.Items);

            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.Output)))
                dataElements.AddRange(_outputDetail.Items);
            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.ScalarOutput)))
                dataElements.AddRange(_outputDetail.Items.Where(p => p.ValueType.IsScalar()));
            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.NonScalarOutput)))
                dataElements.AddRange(_outputDetail.Items.Where(p => !p.ValueType.IsScalar()));

            return dataElements.Distinct().ToList();
        }

        /// <summary>
        /// Returns the entry of the specified kind with the specified unique name.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="taskEntryKinds">The kind end entries to consider.</param>
        /// <returns>Returns the input with the specified name.</returns>
        public IDataElement GetEntryWithName(string key, params TaskEntryKind[] taskEntryKinds)
        {
            return GetEntries(taskEntryKinds).Find(p => p.KeyEquals(key));
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
        public override object Clone()
        {
            BdoTaskConfiguration task = base.Clone() as BdoTaskConfiguration;

            if (_inputDetail != null)
                task.InputDetail = _inputDetail.Clone() as DataElementSet;

            if (_outputDetail != null)
                task.OutputDetail = _outputDetail.Clone() as DataElementSet;

            return task;
        }

        #endregion

        // --------------------------------------------------
        // SERIALIZATION
        // --------------------------------------------------

        #region Serialization

        /// <summary>
        /// Updates information for storage.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateStorageInfo(IBdoLog log = null)
        {
            base.UpdateStorageInfo(log);

            _outputDetail?.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of script variables to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IBdoScope scope = null, IScriptVariableSet scriptVariableSet = null, IBdoLog log = null)
        {
            _outputDetail?.UpdateRuntimeInfo(scope, scriptVariableSet, log);

            base.UpdateRuntimeInfo(scope, scriptVariableSet, log);
        }

        #endregion

        // --------------------------------------------------
        // UPDATE, CHECK, REPAIR
        // --------------------------------------------------

        #region Update_Check_Repair

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override IBdoLog Update<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateModes[] updateModes = null)
        {
            var log = new BdoLog();

            if (item is BdoTaskDefinitionDto)
            {
                BdoTaskDefinitionDto definition = item as BdoTaskDefinitionDto;

                if (OutputDetail != null)
                    log.AddEvents(OutputDetail.Update(definition.OutputSpecification));
            }

            return log;
        }

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <param name="isExistenceChecked">Indicates whether the carrier existence is checked.</param>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <returns>Returns the check log.</returns>
        public override IBdoLog Check<T>(
            bool isExistenceChecked = true,
            T item = default,
            string[] specificationAreas = null)
        {
            var log = new BdoLog();

            if (item is BdoTaskConfiguration configuration)
            {
                log.AddEvents(Check(isExistenceChecked, configuration, specificationAreas));
            }
            return log;
        }

        /// <summary>
        /// Repairs this instance with the specified definition.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <returns>Log of the operation.</returns>
        public override IBdoLog Repair<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateModes[] updateModes = null)
        {
            var log = new BdoLog();

            if (item is BdoTaskDefinitionDto)
            {
                BdoTaskDefinitionDto definition = item as BdoTaskDefinitionDto;

                if (OutputDetail != null)
                    log.AddEvents(OutputDetail.Repair(definition.OutputSpecification));
            }

            return log;
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

            _inputDetail?.Dispose();
            _outputDetail?.Dispose();

            _isDisposed = true;

            if (isDisposing)
            {
                GC.SuppressFinalize(this);
            }

            base.Dispose(isDisposing);
        }

        #endregion
    }
}