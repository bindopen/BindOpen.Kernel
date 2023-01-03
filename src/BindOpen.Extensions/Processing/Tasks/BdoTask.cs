using BindOpen.Data;
using BindOpen.Data.Elements;
using BindOpen.Runtime.Definition;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;
using BindOpen.Logging;

namespace BindOpen.Extensions.Processing
{
    /// <summary>
    /// This class represents an task.
    /// </summary>
    public abstract class BdoTask :
        TBdoExtensionItem<IBdoTaskDefinition, IBdoTaskConfiguration, IBdoTask>,
        IBdoTask
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoTask class.
        /// </summary>
        protected BdoTask() : base()
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
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varElementSet">The variable element set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <param name="taskEntryKinds">The kind end entries to consider.</param>
        public object GetEntryObjectWithName(
            string name,
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            IBdoLog log = null,
            params TaskEntryKind[] taskEntryKinds)
        {
            IBdoElement entry = Configuration?.GetEntryWithName(name, taskEntryKinds);

            return entry?.GetItem(scope, varElementSet, log);
        }

        // General ---------------------------------------

        /// <summary>
        /// Indicates whether this instance has compatible entries with the specified element collection.
        /// </summary>
        /// <param name="dataElementSpecSet">The set of element specifications to consider.</param>
        /// <param name="taskEntryKind">The task entry kind to consider.</param>
        /// <returns>True if this instance is compatible with the specified element collection.</returns>
        public bool IsCompatibleWith(
            IBdoElementSpecSet dataElementSpecSet,
            TaskEntryKind taskEntryKind = TaskEntryKind.Any)
        {
            if (Configuration == null) return false;

            if (dataElementSpecSet == null)
            {
                return true;
            }
            else
            {
                foreach (IBdoElement entry in Configuration.GetEntries(taskEntryKind))
                {
                    IBdoElementSpec dataElementSpec = dataElementSpecSet[entry.Key()];
                    if (dataElementSpec != null)
                    {
                        bool isCompatible = dataElementSpec.IsCompatibleWithItem(entry);
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
        public bool IsConfigurable(SpecificationLevels specificationLevel = SpecificationLevels.Runtime)
        {
            var elements = new List<IBdoElement>();
            elements.AddRange(Configuration?.GetEntries(TaskEntryKind.Input));
            elements.AddRange(Configuration?.GetEntries(TaskEntryKind.ScalarOutput));

            if (elements.Count == 0)
            {
                return false;
            }
            else
            {
                foreach (var element in elements)
                {
                    if (element.GetSpecification("item")?.SpecificationLevels?.ToArray().Has(specificationLevel) == false)
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
        public IBdoTask UpdateAbsolutePaths(string relativePath)
        {
            //foreach (BdoElement currentElement in _Inputs)
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
            //foreach (BdoElement currentElement in _Outputs)
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
            return this;
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
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varElementSet">The variable element set to use for execution.</param>
        /// <param name="runtimeMode">The runtime mode to consider.</param>
        /// <returns>Returns the output value of the execution.</returns>
        public virtual IBdoTask Execute(
            IBdoScope scope = null,
            IBdoElementSet varElementSet = null,
            RuntimeModes runtimeMode = RuntimeModes.Normal,
            IBdoLog log = null)
        {
            return this;
        }

        #endregion
    }
}