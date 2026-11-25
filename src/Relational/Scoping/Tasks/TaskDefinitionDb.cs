using BindOpen.Data.Meta;
using System.Collections.Generic;

namespace BindOpen.Scoping.Tasks
{
    /// <summary>
    /// This class represents a task definition.
    /// </summary>
    /// <seealso cref="ConfigurationDb"/>
    public class TaskDefinitionDb : ExtensionDefinitionDb
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The name of the group of this instance.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Indicates whether this instance is executable.
        /// </summary>
        public bool IsExecutable { get; set; }

        /// <summary>
        /// The item class of this instance.
        /// </summary>
        public string ItemClass { get; set; }

        /// <summary>
        /// The maximum index of this instance.
        /// </summary>
        public float MaximumIndex { get; set; } = 100;

        /// <summary>
        /// The inputs of this instance.
        /// </summary>
        public List<MetaDataDb> InputSpecification { get; set; }

        /// <summary>
        /// The outputs of this instance.
        /// </summary>
        public List<MetaDataDb> OutputSpecification { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TaskDefinitionDb class. 
        /// </summary>
        public TaskDefinitionDb()
        {
        }

        #endregion
    }
}