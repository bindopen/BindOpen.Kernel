using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Commands;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Extensions.Definition.Tasks;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Configuration.Tasks
{
    /// <summary>
    /// This class represents an task.
    /// </summary>
    [Serializable()]
    [XmlType("TaskConfiguration", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "task", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    [XmlInclude(typeof(Command))]
    public class TaskConfiguration : TAppExtensionTitledItemConfiguration<TaskDefinition>
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        // Entries --------------------------------

        private DataElementSet _InputDetail = new DataElementSet();
        private DataElementSet _OutputDetail = new DataElementSet();

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        // Miscellaneous ----------------------------

        /// <summary>
        /// Detail of this instance.
        /// </summary>
        [XmlElement("detail")]
        public DataElementSet Detail { get; set; } = new DataElementSet();

        /// <summary>
        /// Specification of the DetailSpecified property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean DetailSpecified
        {
            get
            {
                return this.Detail != null && (this.Detail.ElementsSpecified || this.Detail.DescriptionSpecified);
            }
        }

        /// <summary>
        /// Maximum index of this instance.
        /// </summary>
        [XmlElement("maximumIndex")]
        [DefaultValue(100)]
        public float MaximumIndex { get; set; } = 100;

        // End points -------------------------------

        /// <summary>
        /// Input detail of this instance.
        /// </summary>
        /// <seealso cref="OutputDetail"/>
        [XmlElement("inputs")]
        public DataElementSet InputDetail
        {
            get
            {
                return this._InputDetail ?? (this._InputDetail = new DataElementSet());
            }
            set
            {
                this._InputDetail = value;
            }
        }

        /// <summary>
        /// Specification of the InputSpecified property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean InputDetailSpecified
        {
            get
            {
                return this._InputDetail != null && (this._InputDetail.ElementsSpecified || this._InputDetail.DescriptionSpecified);
            }
        }

        /// <summary>
        /// Output detail of this instance.
        /// </summary>
        /// <seealso cref="InputDetail"/>
        [XmlElement("outputs")]
        public DataElementSet OutputDetail
        {
            get
            {
                return this._OutputDetail ?? (this._OutputDetail = new DataElementSet());
            }
            set
            {
                this._OutputDetail = value;
            }
        }

        /// <summary>
        /// Specification of the OutputSpecified property of this instance.
        /// </summary>
        [XmlIgnore()]
        public Boolean OutputDetailSpecified
        {
            get
            {
                return this._OutputDetail != null && (this._OutputDetail.ElementsSpecified || this._OutputDetail.DescriptionSpecified);
            }
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TaskConfiguration class.
        /// </summary>
        public TaskConfiguration() : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TaskConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionName">The definition name to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="inputDetail">The input detail to consider.</param>
        public TaskConfiguration(
            String name,
            String definitionName=null,
            String namePreffix = "task_",
            DataElementSet inputDetail = null)
            : base(name, definitionName, null, namePreffix)
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
        public List<DataElement> GetEntries(params TaskEntryKind[] taskEntryKinds)
        {
            if (taskEntryKinds.Length==0)
                taskEntryKinds = new TaskEntryKind[1] { TaskEntryKind.Any };

            List<DataElement> dataElements = new List<DataElement>();
            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.Input)))
                dataElements.AddRange(this._InputDetail.Elements);
            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.Output)))
                dataElements.AddRange(this._OutputDetail.Elements);
            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.ScalarOutput)))
                dataElements.AddRange(this._OutputDetail.Elements.Where(p => p.ValueType.IsScalar()));
            if ((taskEntryKinds.Contains(TaskEntryKind.Any)) || (taskEntryKinds.Contains(TaskEntryKind.ScalarOutput)))
                dataElements.AddRange(this._OutputDetail.Elements.Where(p => p.ValueType.IsScalar()));

            return dataElements;
        }

        /// <summary>
        /// Returns the entry of the specified kind with the specified unique name.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <param name="taskEntryKinds">The kind end entries to consider.</param>
        /// <returns>Returns the input with the specified name.</returns>
        public DataElement GetEntryWithName(string key, params TaskEntryKind[] taskEntryKinds)
        {
            return this.GetEntries(taskEntryKinds).Find(p => p.KeyEquals(key));
        }

        /// <summary>
        /// Gets the item object of the specified entry.
        /// </summary>
        /// <param name="name">The name of the entry to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <param name="taskEntryKinds">The kind end entries to consider.</param>
        public Object GetEntryItemObjectWithName(
            String name,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null,
            params TaskEntryKind[] taskEntryKinds)
        {
            DataElement entry = this.GetEntryWithName(name, taskEntryKinds);

            return entry?.GetItemObject(appScope, scriptVariableSet, log);
        }

        // General ---------------------------------------

        /// <summary>
        /// Indicates whether this instance has compatible entries with the specified element collection.
        /// </summary>
        /// <param name="dataElementSpecSet">The set of element specifications to consider.</param>
        /// <param name="taskEntryKind">The task entry kind to consider.</param>
        /// <returns>True if this instance is compatible with the specified element collection.</returns>
        public Boolean IsCompatibleWith(
            DataElementSpecSet dataElementSpecSet,
            TaskEntryKind taskEntryKind = TaskEntryKind.Any)
        {
            if (dataElementSpecSet == null)
                return true;
            else
                foreach (DataElement endPoint in this.GetEntries(taskEntryKind))
                {
                    DataElementSpec dataElementSpec = dataElementSpecSet[endPoint.Key()];
                    if (dataElementSpec != null)
                    {
                        Boolean isCompatible = endPoint.IsCompatibleWith(dataElementSpec);
                        if (!isCompatible) return false;
                    }
                }

            return false;
        }

        /// <summary>
        /// Indicates whether this instance has compatible entries with the specified elements.
        /// </summary>
        /// <param name="dataElementSpecs">The data elements to consider.</param>
        /// <param name="taskEntryKind">The kind end entry to consider.</param>
        /// <returns>True if this instance is compatible with the specified data elements.</returns>
        public Boolean IsCompatibleWith(
            List<DataElementSpec> dataElementSpecs,
            TaskEntryKind taskEntryKind = TaskEntryKind.Any)
        {
            if (dataElementSpecs == null)
                return true;

            return this.IsCompatibleWith(
                new DataElementSpecSet(dataElementSpecs.ToArray()),
                taskEntryKind);
        }

        /// <summary>
        /// Indicates whether this instance is configurable.
        /// </summary>
        /// <returns>True if this instance is configurable.</returns>
        public Boolean IsConfigurable(SpecificationLevel specificationLevel = SpecificationLevel.Runtime)
        {
            List<DataElement> dataElements = new List<DataElement>();
            dataElements.AddRange(this.GetEntries(TaskEntryKind.Input));
            dataElements.AddRange(this.GetEntries(TaskEntryKind.ScalarOutput));

            if (dataElements.Count == 0)
                return false;
            else
                foreach (DataElement dataElement in dataElements)
                    if ((dataElement.Specification != null) &&
                        (!dataElement.Specification.GetAreaSpecification("item").SpecificationLevels.Has(specificationLevel)))
                        return false;

            return true;
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Updates the relevant property based on the specified object attribute.
        /// </summary>
        /// <param name="dataElement">The object attribute to consider.</param>
        /// <param name="taskEntryKind">The task entry kind to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>The log of the update task.</returns>
        private Log UpdateProperty(
            DataElement dataElement,
            TaskEntryKind taskEntryKind,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = new Log();

            if (dataElement != null)
                try
                {
                    PropertyInfo propertyInfo = this.GetType().GetProperty(dataElement.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (propertyInfo != null)
                            propertyInfo.SetValue(dataElement.GetItemObject(appScope, scriptVariableSet, log), null);
                    else
                        log.AddError(
                            taskEntryKind.GetTitle() + " '" + dataElement.Key() + "' unknown in task '" +
                            (this.Definition != null ? this.Definition.Key() : "undefined") + "'");
                }
                catch
                {
                    log.AddError(
                        "Error occured while setting " + taskEntryKind.GetTitle() +
                        "'" + dataElement.Name + "' unknown in task '" +
                        (this.Definition != null ? this.Definition.Key() : "undefined") + "'");
                }

            return log;
        }

        /// <summary>
        /// Updates the input properties from input detail.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>The log of the update task.</returns>
        protected Log UpdateInputPropertiesFromInputDetail(
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = new Log();

            foreach (DataElement dataElement in this.GetEntries(TaskEntryKind.Input))
                log.AddEvents(this.UpdateProperty(dataElement, TaskEntryKind.Input));

            return log;
        }

        /// <summary>
        /// Updates the non-value output properties from output detail.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>The log of the update task.</returns>
        protected Log UpdateNonScalarOutputPropertiesFromOutputDetail(
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = new Log();

            foreach (DataElement dataElement in this.GetEntries(TaskEntryKind.NonScalarOutput))
                log.AddEvents(this.UpdateProperty(dataElement, TaskEntryKind.NonScalarOutput));

            return log;
        }

        /// <summary>
        /// Updates output detail from the output properties.
        /// </summary>
        /// <returns>The log of the update task.</returns>
        protected Log UpdateOutputDetailFromOutputProperties()
        {
            Log log = new Log();

            Type aType = this.GetType();
            foreach (DataElement dataElement in this.OutputDetail.Elements)
            {
                PropertyInfo aInputProperty = aType.GetProperty(dataElement.Name);
                if (aInputProperty != null)
                    try
                    {
                        dataElement.SetItem(aInputProperty.GetValue(this, null));
                    }
                    catch
                    {
                        log.AddError(
                            "Bad value type (sigle value/list) defined for the output property named '" + dataElement.Key() + "' of business task called '" + this.Key() + "'");
                    }
                else
                    log.AddError(
                        "Output property named '" + dataElement.Key() + "' of business task called '" + this.Key() + "' not existing");
            }

            return log;
        }

        /// <summary>
        /// Updates the relative paths of this instance.
        /// </summary>
        /// <param name="relativePath">The relative path to consider.</param>
        public void UpdateAbsolutePaths(String relativePath)
        {
            //foreach (DataElement currentDataElement in this._Inputs)
            //    if (currentDataElement.CarrierKind == DocumentKind.RepositoryFile)
            //    {
            //        RepositoryFile aRepositoryFile = (RepositoryFile)currentDataElement.GetValue();
            //        if (aRepositoryFile != null)
            //        {
            //            aRepositoryFile.Path = RepositoryFile.GetAbsolutePath(aRepositoryFile.Path, relativePath);
            //            aRepositoryFile.Paths = RepositoryFile.GetAbsolutePath(aRepositoryFile.Paths, relativePath);
            //            currentDataElement.SetValue(aRepositoryFile);
            //        }
            //    }
            //foreach (DataElement currentDataElement in this._Outputs)
            //    if (currentDataElement.CarrierKind == DocumentKind.RepositoryFile)
            //    {
            //        RepositoryFile aRepositoryFile = (RepositoryFile)currentDataElement.GetValue();
            //        if (aRepositoryFile != null)
            //        {
            //            aRepositoryFile.Path = RepositoryFile.GetAbsolutePath(aRepositoryFile.Path, relativePath);
            //            aRepositoryFile.Paths = RepositoryFile.GetAbsolutePath(aRepositoryFile.Paths, relativePath);
            //            currentDataElement.SetValue(aRepositoryFile);
            //        }
            //    }
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
        public override Object Clone()
        {
            TaskConfiguration task = base.Clone() as TaskConfiguration;
            if (this.Detail != null)
                task.Detail = this.Detail.Clone() as DataElementSet;
            if (this._InputDetail != null)
                task.InputDetail = this._InputDetail.Clone() as DataElementSet;
            if (this._OutputDetail != null)
                task.OutputDetail = this._OutputDetail.Clone() as DataElementSet;

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
        public override void UpdateStorageInfo(Log log = null)
        {
            base.UpdateStorageInfo(log);

            this.Detail?.UpdateStorageInfo(log);

            this._InputDetail?.UpdateStorageInfo(log);
            this._OutputDetail?.UpdateStorageInfo(log);
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null,  Log log = null)
        {
            base.UpdateRuntimeInfo(appScope, log);

            this.Detail?.UpdateRuntimeInfo(appScope, log);
            this._InputDetail?.UpdateRuntimeInfo(appScope, log);
            this._OutputDetail?.UpdateRuntimeInfo(appScope, log);
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override Log Update<T>(
            T item = null,
            List<String> specificationAreas = null,
            List<UpdateMode> updateModes = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = new Log();

            if (item is TaskDefinition)
            {
                TaskDefinition definition = item as TaskDefinition;
                if (this.InputDetail != null)
                    log.Append(this.InputDetail.Update(definition.InputSpecification));
                if (this.OutputDetail != null)
                    log.Append(this.OutputDetail.Update(definition.OutputSpecification));
            }

            return log;
        }

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <param name="isExistenceChecked">Indicates whether the carrier existence is checked.</param>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Returns the check log.</returns>
        public override Log Check<T>(
            Boolean isExistenceChecked = true,
            T item = null,
            List<String> specificationAreas = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            return base.Check(isExistenceChecked, item, specificationAreas, appScope, scriptVariableSet);
        }

        /// <summary>
        /// Repairs this instance with the specified definition.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Log of the operation.</returns>
        public override Log Repair<T>(
            T item = null,
            List<String> specificationAreas = null,
            List<UpdateMode> updateModes = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = new Log();

            if (item is TaskDefinition)
            {
                TaskDefinition definition = item as TaskDefinition;
                if (this.InputDetail != null)
                    log.Append(this.InputDetail.Repair(definition.InputSpecification));
                if (this.OutputDetail != null)
                    log.Append(this.OutputDetail.Repair(definition.OutputSpecification));
            }

            return log;
        }

        #endregion
    }
}