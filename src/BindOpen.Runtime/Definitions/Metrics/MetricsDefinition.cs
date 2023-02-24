using System;

namespace BindOpen.Runtime.Definitions
{
    /// <summary>
    /// This class represents a metrics definition.
    /// </summary>
    public class BdoMetricsDefinition : BdoExtensionDefinition, IBdoMetricsDefinition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Item class of this instance.
        /// </summary>
        public string ItemClass { get; set; }

        /// <summary>
        /// The runtime type of this instance.
        /// </summary>
        public Type RuntimeType { get; set; }

        /// <summary>
        /// The unique ID of this instance.
        /// </summary>
        public new string UniqueName { get => PackageDefinition?.UniqueName + "$" + Name; }

        /// <summary>
        /// Unit code of this instance.
        /// </summary>
        public string UnitCode { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the MetricsDefinition class. 
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="extensionDefinition">The extensition definition to consider.</param>
        public BdoMetricsDefinition(
            string name,
            IBdoPackageDefinition extensionDefinition) : base(name, "metricsDef_", extensionDefinition)
        {
        }

        #endregion
    }
}