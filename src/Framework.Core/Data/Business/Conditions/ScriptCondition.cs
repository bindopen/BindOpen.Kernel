using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Expression;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.Business.Conditions
{
    /// <summary>
    /// This class represents a script condition.
    /// </summary>
    [Serializable()]
    [XmlType("ScriptBusinessCondition", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "script.condition", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class ScriptCondition : Condition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Expression script representing the condition.
        /// </summary>
        [XmlElement("expression")]
        public DataExpression Expression { get; set; } = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BusinessScriptCondition class.
        /// </summary>
        public ScriptCondition()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BusinessScriptCondition class.
        /// </summary>
        /// <param name="trueValue">The true value to consider.</param>
        /// <param name="expression">The expression to consider.</param>
        public ScriptCondition(string trueValue, DataExpression expression) : base(trueValue)
        {
            this.Expression = expression;
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Clones this instance.
        /// </summary>
        public override Object Clone()
        {
            ScriptCondition condition = new ScriptCondition
            {
                Expression = this.Expression.Clone() as DataExpression
            };

            return condition;
        }

        #endregion

        // ------------------------------------------
        // PROCESS
        // ------------------------------------------

        #region Process

        /// <summary>
        /// Evaluate this instance.
        /// </summary>
        /// <param name="scriptInterpreter">Script interpreter.</param>
        /// <param name="scriptVariableSet">The script variable set used to evaluate.</param>
        /// <returns>True if the business script value is the true value.</returns>
        public override Boolean Evaluate(
            ScriptInterpreter scriptInterpreter,
            ScriptVariableSet scriptVariableSet)
        {
            if (this.Expression == null)
                return false;

            return (scriptInterpreter.Interprete(this.Expression, scriptVariableSet) ?? "").ToUpper().Trim() == this.TrueValue.ToUpper().Trim();
        }

        #endregion
    }
}