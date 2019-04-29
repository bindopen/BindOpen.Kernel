using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Items.Tasks;
using BindOpen.Framework.Core.Extensions.Items.Tasks.Definition;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Items.Tasks
{
    /// <summary>
    /// This class represents a task configuration.
    /// </summary>
    [Serializable()]
    [XmlType("TaskConfiguration", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "task", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public class TaskConfiguration
        : TAppExtensionTitledItemConfiguration<TaskDefinition>, ITaskConfiguration
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
        /// Specification of the InputSpecified property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool InputDetailSpecified => _inputDetail != null && (_inputDetail.ElementsSpecified || _inputDetail.DescriptionSpecified);

        /// <summary>
        /// Output detail of this instance.
        /// </summary>
        [XmlElement("outputs")]
        public DataElementSet OutputDetail
        {
            get => _outputDetail ?? (_outputDetail = new DataElementSet());
            set => _outputDetail = value;
        }

        /// <summary>
        /// Specification of the OutputSpecified property of this instance.
        /// </summary>
        [XmlIgnore()]
        public bool OutputDetailSpecified => _outputDetail != null && (_outputDetail.ElementsSpecified || _outputDetail.DescriptionSpecified);

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TaskConfiguration class.
        /// </summary>
        public TaskConfiguration() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TaskConfiguration class.
        /// </summary>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        /// <param name="items">The items to consider.</param>
        public TaskConfiguration(
            string definitionUniqueId,
            params IDataElement[] items)
            : base(AppExtensionItemKind.Task, definitionUniqueId, items)
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
            if (taskEntryKinds.Length==0)
                taskEntryKinds = new TaskEntryKind[1] { TaskEntryKind.Any };

            List<IDataElement> dataElements = new List<IDataElement>();

            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.Input)))
                dataElements.AddRange(_inputDetail.Elements);

            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.Output)))
                dataElements.AddRange(_outputDetail.Elements);
            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.ScalarOutput)))
                dataElements.AddRange(_outputDetail.Elements.Where(p => p.ValueType.IsScalar()));
            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.NonScalarOutput)))
                dataElements.AddRange(_outputDetail.Elements.Where(p => !p.ValueType.IsScalar()));

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
            TaskConfiguration task = base.Clone() as TaskConfiguration;

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
        public override void UpdateStorageInfo(ILog log = null)
        {
            base.UpdateStorageInfo(log);

            _outputDetail?.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null)
        {
            base.UpdateRuntimeInfo(appScope, scriptVariableSet, log);

            _outputDetail?.UpdateRuntimeInfo(appScope, scriptVariableSet, log);
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
        public override ILog Update<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateMode[] updateModes = null)
        {
            ILog log = new Log();

            if (item is TaskDefinitionDto)
            {
                TaskDefinitionDto definition = item as TaskDefinitionDto;

                if (OutputDetail != null)
                    log.Append(OutputDetail.Update(definition.OutputSpecification));
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
        public override ILog Check<T>(
            bool isExistenceChecked = true,
            T item = default,
            string[] specificationAreas = null)
        {
            ILog log = new Log();

            if (item is TaskConfiguration configuration)
            {
                log.Append(Check(isExistenceChecked, configuration, specificationAreas));
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
        public override ILog Repair<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateMode[] updateModes = null)
        {
            ILog log = new Log();

            if (item is TaskDefinitionDto)
            {
                TaskDefinitionDto definition = item as TaskDefinitionDto;

                if (OutputDetail != null)
                    log.Append(OutputDetail.Repair(definition.OutputSpecification));
            }

            return log;
        }

        #endregion
    }
}