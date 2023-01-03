using System;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This class represents a metrics definition.
    /// </summary>
    public class BdoMetricsDefinition : BdoExtensionItemDefinition, IBdoMetricsDefinition
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
        public new string UniqueId { get => ExtensionDefinition?.UniqueId + "$" + Name; }

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
        /// <param name="name">The name to consider.</param>
        /// <param name="extensionDefinition">The extensition definition to consider.</param>
        public BdoMetricsDefinition(
            string name,
            IBdoExtensionDefinition extensionDefinition) : base(name, "metricsDef_", extensionDefinition)
        {
        }

        #endregion
    }
}