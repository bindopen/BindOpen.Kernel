namespace BindOpen.System.Data.Conditions
{

    /// <summary>
    /// This class represents an basic condition.
    /// </summary>
    public class BdoBasicCondition : BdoCondition, IBdoBasicCondition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The arugment 1 of this instance.
        /// </summary>
        public object Argument1 { get; set; }

        /// <summary>
        /// The operator of this instance.
        /// </summary>
        public ConditionOperator Operator { get; set; }

        /// <summary>
        /// The arugment 2 of this instance.
        /// </summary>
        public object Argument2 { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BasicCondition class.
        /// </summary>
        public BdoBasicCondition()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BasicCondition class.
        /// </summary>
        /// <param key="arg1">The argument 1 to consider.</param>
        /// <param key="ope">The operator to consider.</param>
        /// <param key="arg2">The argument 2 to consider.</param>
        public BdoBasicCondition(
            object arg1,
            ConditionOperator ope,
            object arg2 = null)
        {
            Argument1 = arg1;
            Argument2 = arg2;
            Operator = ope;
        }

        #endregion
    }
}