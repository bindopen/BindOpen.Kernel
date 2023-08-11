namespace BindOpen.System.Data.Conditions
{
    /// <summary>
    /// This class represents a script condition.
    /// </summary>
    public class BdoReferenceCondition : BdoCondition, IBdoReferenceCondition
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ReferenceCondition class.
        /// </summary>
        public BdoReferenceCondition()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the ReferenceCondition class.
        /// </summary>
        /// <param key="trueValue">The true value to consider.</param>
        /// <param key="exp">The exp to consider.</param>
        public BdoReferenceCondition(
            IBdoReference exp) : base()
        {
            DataReference = exp;
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
            var condition = new BdoReferenceCondition
            {
                DataReference = DataReference?.Clone<BdoReference>()
            };

            return condition;
        }

        #endregion

        // ------------------------------------------
        // IReferenceCondition Implementation
        // ------------------------------------------

        #region IReferenceCondition

        /// <summary>
        /// Expression script representing the condition.
        /// </summary>
        public IBdoReference DataReference { get; set; }

        #endregion
    }
}