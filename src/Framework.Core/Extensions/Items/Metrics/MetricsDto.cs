using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Expression;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Definition.Metrics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Items.Metrics
{
    /// <summary>
    /// This class represents a metrics configuration.
    /// </summary>
    [Serializable()]
    [XmlType("MetricsDto", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "metrics", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class MetricsDto
        : TAppExtensionTitledItemDto<MetricsDefinitionDto>, IMetricsDto
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
        /// Instantiates a new instance of the MetricsDto class.
        /// </summary>
        public MetricsDto() : this(null, null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the MetricsDto class.
        /// </summary>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        /// <param name="valueScript">The value script to consider.</param>
        /// <param name="items">The items to consider.</param>
        public MetricsDto(
            string definitionUniqueId,
            string valueScript = null,
            params IDataElement[] items)
            : base(AppExtensionItemKind.Metrics, definitionUniqueId, items)
        {
            ValueScript = valueScript;
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
        public virtual int? GetValue(
            IScriptInterpreter scriptInterpreter,
            IScriptVariableSet scriptVariableSet = null)
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
            MetricsDto dataMetrics = base.Clone() as MetricsDto;
            return dataMetrics;
        }

        #endregion
    }
}