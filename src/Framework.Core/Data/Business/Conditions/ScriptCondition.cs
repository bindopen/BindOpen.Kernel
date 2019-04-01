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
    public class ScriptCondition : Condition, IScriptCondition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Expression script representing the condition.
        /// </summary>
        [XmlElement("expression")]
        public IDataExpression Expression { get; set; } = null;

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
        public ScriptCondition(bool trueValue, IDataExpression expression) : base(trueValue)
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
        public override object Clone()
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
        public override bool Evaluate(
            IScriptInterpreter scriptInterpreter,
            IScriptVariableSet scriptVariableSet)
        {
            if (this.Expression == null)
                return false;

            string st = scriptInterpreter.Interprete(this.Expression, scriptVariableSet);
            return TrueValue ?
                string.Compare(st, "%true()", StringComparison.OrdinalIgnoreCase) == 0 :
                string.Compare(st, "%false()", StringComparison.OrdinalIgnoreCase) == 0;
        }

        #endregion
    }
}