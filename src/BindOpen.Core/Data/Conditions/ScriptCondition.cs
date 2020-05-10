using BindOpen.Data.Expression;
using System.Xml.Serialization;

namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// This class represents a script condition.
    /// </summary>
    [XmlType("ScriptBusinessCondition", Namespace = "https://docs.bindopen.org/xsd")]
    [XmlRoot(ElementName = "script.condition", Namespace = "https://docs.bindopen.org/xsd", IsNullable = false)]
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
        public ScriptCondition(bool trueValue, IDataExpression expression) : base(trueValue)
        {
            this.Expression = expression as DataExpression;
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Clones this instance.
        /// </summary>
        public override object Clone(params string[] areas)
        {
            ScriptCondition condition = new ScriptCondition
            {
                Expression = this.Expression.Clone() as DataExpression
            };

            return condition;
        }

        #endregion
    }
}