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
    public class MetricsConfiguration : TAppExtensionTitledItemConfiguration<MetricsDefinition>
    {

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private String _ValueScript = "";

        #endregion


        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Value script of this instance.
        /// </summary>
        [XmlElement("valueScript")]
        public String ValueScript
        {
            get { return this._ValueScript; }
            set { this._ValueScript = value; }
        }

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the MetricsConfiguration class.
        /// </summary>
        public MetricsConfiguration()
             : this(null)
        {
        }

        /// <summary>
        /// This instantiates a new instance of the MetricsConfiguration class.
        /// </summary>
        /// <param name="name">The name of this instance.</param>
        /// <param name="definitionName">The definition name to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        public MetricsConfiguration(
            String name,
            String definitionName = null,
            String namePreffix = "metrics_")
            : base(name, definitionName, null, namePreffix)
        {
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
        public virtual int? GetValue(ScriptInterpreter scriptInterpreter, ScriptVariableSet scriptVariableSet = null)
        {
            String stringValue = 
                (scriptInterpreter==null ? null :
                scriptInterpreter.Interprete(new DataExpression(this.ValueScript, DataExpressionKind.Script), scriptVariableSet));
            int? value = null;
            int aIntValue = -1;
            if (int.TryParse(stringValue ?? "", out aIntValue))
                value = new int?(aIntValue);

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
        public override Object Clone()
        {
            MetricsConfiguration dataMetrics = base.Clone() as MetricsConfiguration;
            return dataMetrics;
        }

        #endregion

    }
}