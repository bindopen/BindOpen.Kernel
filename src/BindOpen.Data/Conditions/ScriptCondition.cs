namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// This class represents a script condition.
    /// </summary>
    public class ScriptCondition : Condition, IScriptCondition
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ScriptCondition class.
        /// </summary>
        public ScriptCondition()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ScriptCondition class.
        /// </summary>
        /// <param key="trueValue">The true value to consider.</param>
        /// <param key="exp">The exp to consider.</param>
        public ScriptCondition(bool trueValue, IBdoExpression exp) : base(trueValue)
        {
            Expression = exp;
        }

        #endregion

        // ------------------------------------------
        // IDataItem Implementation
        // ------------------------------------------

        #region IDataItem

        /// <summary>
        /// Clones this instance.
        /// </summary>
        public override object Clone(params string[] areas)
        {
            var condition = new ScriptCondition
            {
                Expression = Expression.Clone<BdoExpression>()
            };

            return condition;
        }

        #endregion

        // ------------------------------------------
        // IScriptCondition Implementation
        // ------------------------------------------

        #region IScriptCondition

        /// <summary>
        /// Expression script representing the condition.
        /// </summary>
        public IBdoExpression Expression { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="exp"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IScriptCondition WithExpression(IBdoExpression exp)
        {
            Expression = exp;
            return this;
        }

        #endregion
    }
}