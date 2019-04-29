using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Extensions.Items.Tasks;
using BindOpen.Framework.Core.Extensions.Items.Tasks.Definition;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Items.Tasks
{
    /// <summary>
    /// This class represents an task.
    /// </summary>
    public abstract class Task : TAppExtensionItem<ITaskDefinition>, ITask
    {
        new public ITaskConfiguration Configuration { get => base.Configuration as ITaskConfiguration; }

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Task class.
        /// </summary>
        protected Task() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Task class.
        /// </summary>
        /// <param name="dto">The DTO item of this instance.</param>
        protected Task(ITaskConfiguration dto)
        {
        }

        #endregion

        //------------------------------------------
        // ACCESSORS
        //-----------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the item object of the specified entry.
        /// </summary>
        /// <param name="name">The name of the entry to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <param name="taskEntryKinds">The kind end entries to consider.</param>
        public object GetEntryObjectWithName(
            string name,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null,
            params TaskEntryKind[] taskEntryKinds)
        {
            IDataElement entry = Configuration?.GetEntryWithName(name, taskEntryKinds);

            return entry?.GetObject(appScope, scriptVariableSet, log);
        }

        // General ---------------------------------------

        /// <summary>
        /// Indicates whether this instance has compatible entries with the specified element collection.
        /// </summary>
        /// <param name="dataElementSpecSet">The set of element specifications to consider.</param>
        /// <param name="taskEntryKind">The task entry kind to consider.</param>
        /// <returns>True if this instance is compatible with the specified element collection.</returns>
        public bool IsCompatibleWith(
            IDataElementSpecSet dataElementSpecSet,
            TaskEntryKind taskEntryKind = TaskEntryKind.Any)
        {
            if (Configuration == null) return false;

            if (dataElementSpecSet == null)
            {
                return true;
            }
            else
            {
                foreach (IDataElement entry in Configuration.GetEntries(taskEntryKind))
                {
                    IDataElementSpec dataElementSpec = dataElementSpecSet[entry.Key()];
                    if (dataElementSpec != null)
                    {
                        bool isCompatible = dataElementSpec.IsCompatibleWith(entry);
                        if (!isCompatible) return false;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Indicates whether this instance is configurable.
        /// </summary>
        /// <returns>True if this instance is configurable.</returns>
        public bool IsConfigurable(SpecificationLevel specificationLevel = SpecificationLevel.Runtime)
        {
            List<IDataElement> elements = new List<IDataElement>();
            elements.AddRange(Configuration?.GetEntries(TaskEntryKind.Input));
            elements.AddRange(Configuration?.GetEntries(TaskEntryKind.ScalarOutput));

            if (elements.Count == 0)
            {
                return false;
            }
            else
            {
                foreach (DataElement element in elements)
                {
                    if (element.Specification?.GetAreaSpecification("item").SpecificationLevels.ToArray().Has(specificationLevel) == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion

        //------------------------------------------
        // MUTATORS
        //-----------------------------------------

        #region Mutators

        /// <summary>
        /// Updates the relative paths of this instance.
        /// </summary>
        /// <param name="relativePath">The relative path to consider.</param>
        public void UpdateAbsolutePaths(string relativePath)
        {
            //foreach (DataElement currentElement in _Inputs)
            //    if (currentElement.CarrierKind == DocumentKind.RepositoryFile)
            //    {
            //        RepositoryFile aRepositoryFile = (RepositoryFile)currentElement.GetValue();
            //        if (aRepositoryFile != null)
            //        {
            //            aRepositoryFile.Path = RepositoryFile.GetAbsolutePath(aRepositoryFile.Path, relativePath);
            //            aRepositoryFile.Paths = RepositoryFile.GetAbsolutePath(aRepositoryFile.Paths, relativePath);
            //            currentElement.SetValue(aRepositoryFile);
            //        }
            //    }
            //foreach (DataElement currentElement in _Outputs)
            //    if (currentElement.CarrierKind == DocumentKind.RepositoryFile)
            //    {
            //        RepositoryFile aRepositoryFile = (RepositoryFile)currentElement.GetValue();
            //        if (aRepositoryFile != null)
            //        {
            //            aRepositoryFile.Path = RepositoryFile.GetAbsolutePath(aRepositoryFile.Path, relativePath);
            //            aRepositoryFile.Paths = RepositoryFile.GetAbsolutePath(aRepositoryFile.Paths, relativePath);
            //            currentElement.SetValue(aRepositoryFile);
            //        }
            //    }
        }

        #endregion

        //------------------------------------------
        // EXECUTION
        //-----------------------------------------

        #region Execution

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use for execution.</param>
        /// <param name="runtimeMode">The runtime mode to consider.</param>
        /// <returns>Returns the output value of the execution.</returns>
        public abstract void Execute(
            ILog log,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            RuntimeMode runtimeMode = RuntimeMode.Normal);

        #endregion
    }
}