namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// This class represents a condition.
    /// </summary>
    public abstract class Condition : BdoItem, ICondition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The value that expresses that the condition is satisfied.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The value that expresses that the condition is satisfied.
        /// </summary>
        public bool TrueValue { get; set; } = true;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Condition class.
        /// </summary>
        protected Condition() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Condition class.
        /// </summary>
        /// <param key="trueValue">The true value to consider.</param>
        protected Condition(bool trueValue) : base()
        {
            this.TrueValue = trueValue;
        }

        #endregion

        // ------------------------------------------
        // IReferenced Implementation
        // ------------------------------------------

        #region IReferenced

        public string Key() => Name;

        #endregion
    }
}