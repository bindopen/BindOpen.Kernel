namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// This class represents a condition.
    /// </summary>
    public abstract class Condition : BdoObject, ICondition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The value that expresses that the condition is satisfied.
        /// </summary>
        public string Name { get; set; }

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

        #endregion

        // ------------------------------------------
        // IReferenced Implementation
        // ------------------------------------------

        #region IReferenced

        public string Key() => Name;

        #endregion
    }
}