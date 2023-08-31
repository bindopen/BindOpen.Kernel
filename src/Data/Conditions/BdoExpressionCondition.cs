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

        // -----------------------------------------------
        // Converters
        // -----------------------------------------------

        #region Converters

        /// <summary>
        /// Converts from string.
        /// </summary>
        /// <param key="st">The string to consider.</param>
        public static explicit operator string(BdoExpressionCondition condition)
        {
            return condition?.Expression?.ToString();
        }

        /// <summary>
        /// Converts from string.
        /// </summary>
        /// <param key="st">The string to consider.</param>
        public static explicit operator BdoExpressionCondition(string script)
        {
            return BdoData.NewCondition(script);
        }

        /// <summary>
        /// Converts from string.
        /// </summary>
        /// <param key="st">The string to consider.</param>
        public static explicit operator BdoExpression(BdoExpressionCondition condition)
        {
            return condition?.Expression as BdoExpression;
        }

        /// <summary>
        /// Converts from string.
        /// </summary>
        /// <param key="st">The string to consider.</param>
        public static explicit operator BdoExpressionCondition(BdoExpression exp)
        {
            return BdoData.NewCondition(exp);
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