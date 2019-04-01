using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Expression;
using BindOpen.Framework.Core.Extensions.Definition.Metrics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Configuration.Metrics
{
    /// <summary>
    /// This class represents a metrics configuration.
    /// </summary>
    [Serializable()]
    [XmlType("MetricsConfiguration", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "metrics", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class MetricsConfiguration : TAppExtensionTitledItemConfiguration<IMetricsDefinition>, IMetricsConfiguration
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Value script of this instance.
        /// </summary>
        [XmlElement("valueScript")]
        public string ValueScript { get; set; } = "";

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the MetricsConfiguration class.
        /// </summary>
        public MetricsConfiguration()
            : base(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the MetricsConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definition">The definition to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        protected MetricsConfiguration(
            string name,
            IMetricsDefinition definition = default,
            string namePreffix = "metrics_")
            : this(name, definition?.Key(), namePreffix)
        {
            _definition = definition;
        }

        /// <summary>
        /// Instantiates a new instance of the MetricsConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        protected MetricsConfiguration(
            string name,
            string definitionUniqueId,
            string namePreffix = "metrics_")
            : base(name, definitionUniqueId, namePreffix)
        {
            DefinitionUniqueId = definitionUniqueId;
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the value of this instance.
        /// </summary>
        /// <param name="scriptInterpreter">The script interpreter to consider.</param>
        /// <param name="scriptVariableSet">The interpretation variables to consider.</param>
        /// <returns>Returns the value of this instance.</returns>
        public virtual int? GetValue(IScriptInterpreter scriptInterpreter, IScriptVariableSet scriptVariableSet = null)
        {
            string stringValue = scriptInterpreter?.Interprete(ValueScript.CreateScript(), scriptVariableSet);
            int? value = null;
            int intValue = -1;
            if (int.TryParse(stringValue ?? "", out intValue))
                value = new int?(intValue);

            return value;
        }

        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned metrics definition.</returns>
        public override object Clone()
        {
            MetricsConfiguration dataMetrics = base.Clone() as MetricsConfiguration;
            return dataMetrics;
        }

        #endregion
    }
}