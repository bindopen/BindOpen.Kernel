using BindOpen.Framework.Core.Data.Expression;
using BindOpen.Framework.Core.System.Scripting;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Conditions
{

    /// <summary>
    /// This class represents a business condition.
    /// </summary>
    [Serializable()]
    [XmlType("BusinessScriptCondition", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "businessScriptCondition", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class BusinessScriptCondition : BusinessCondition
    {

        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private DataExpression _Expression = null;

        #endregion


        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Expression script representing the condition.
        /// </summary>
        [XmlElement("expression")]
        public DataExpression Expression
        {
            get { return this._Expression; }
            set { this._Expression = value; }
        }

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BusinessScriptCondition class.
        /// </summary>
        public BusinessScriptCondition()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BusinessScriptCondition class.
        /// </summary>
        public BusinessScriptCondition(String aTrueValue, DataExpression aExpression) : base(aTrueValue)
        {
            this._Expression = aExpression;
        }

        #endregion


        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region

        /// <summary>
        /// Clones this instance.
        /// </summary>
        public override Object Clone()
        {
            BusinessScriptCondition aBusinessScriptCondition = new BusinessScriptCondition();
            aBusinessScriptCondition.Expression = this.Expression.Clone() as DataExpression;

            return aBusinessScriptCondition;
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
            if (this._Expression == null)
                return false;

            return (scriptInterpreter.Interprete(this._Expression, scriptVariableSet) ?? "").ToString().ToUpper().Trim()
                == this.TrueValue.ToUpper().Trim();
        }

        #endregion


    }
}