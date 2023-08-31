namespace BindOpen.System.Data.Conditions
{
    /// <summary>
    /// This class represents a script condition.
    /// </summary>
    public class BdoExpressionCondition : BdoCondition, IBdoExpressionCondition
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ExpressionCondition class.
        /// </summary>
        public BdoExpressionCondition()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ExpressionCondition class.
        /// </summary>
        /// <param key="trueValue">The true value to consider.</param>
        /// <param key="exp">The exp to consider.</param>
        public BdoExpressionCondition(IBdoExpression exp) : base()
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
        public override object Clone()
        {
            var condition = new BdoExpressionCondition
            {
                Expression = Expression?.Clone<BdoExpression>()
            };

            return condition;
        }

        #endregion

        // ------------------------------------------
        // IExpressionCondition Implementation
        // ------------------------------------------

        #region IExpressionCondition

        /// <summary>
        /// Expression script representing the condition.
        /// </summary>
        public IBdoExpression Expression { get; set; }

        #endregion
    }
}