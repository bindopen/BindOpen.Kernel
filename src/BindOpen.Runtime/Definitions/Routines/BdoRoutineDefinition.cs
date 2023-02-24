using BindOpen.Data;
using BindOpen.Data.Meta;
using System;
using System.Collections.Generic;

namespace BindOpen.Runtime.Definitions
{
    /// <summary>
    /// This class represents a routine definition.
    /// </summary>
    public class BdoRoutineDefinition : BdoExtensionDefinition, IBdoRoutineDefinition
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The item class of this instance.
        /// </summary>
        public string ItemClass { get; set; }

        /// <summary>
        /// The output result codes of this instance.
        /// </summary>
        public List<IDescribed> OutputResultCodes { get; set; } = new List<IDescribed>();

        /// <summary>
        /// The parameter statement of this instance.
        /// </summary>
        public IBdoMetaSet ParameterStatement { get; set; } = new BdoMetaSet();

        /// <summary>
        /// The runtime type of this instance.
        /// </summary>
        public Type RuntimeType { get; set; }

        /// <summary>
        /// The unique ID of this instance.
        /// </summary>
        public new string UniqueName { get => PackageDefinition?.UniqueName + "$" + Name; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RoutineDefinition class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="extensionDefinition">The extensition definition to consider.</param>
        public BdoRoutineDefinition(
            string name,
            IBdoPackageDefinition extensionDefinition) : base(name, "routine_", extensionDefinition)
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
            return UniqueName;
        }

        #endregion
    }
}
