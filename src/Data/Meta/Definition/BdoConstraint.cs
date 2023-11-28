using BindOpen.Kernel.Data.Conditions;
using BindOpen.Kernel.Logging;

namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public class BdoConstraint : BdoObject, IBdoConstraint
    {
        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// The mode.
        /// </summary>
        public BdoConstraintModes Mode { get; set; }

        /// <summary>
        /// The group identifier.
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// The group identifier.
        /// </summary>
        public IBdoReference Reference { get; set; }

        /// <summary>
        /// The result code.
        /// </summary>
        public string ResultCode { get; set; }

        public EventKinds ResultEventKind { get; set; }

        public string ResultTitle { get; set; }

        public string ResultDescription { get; set; }

        /// <summary>
        /// The value.
        /// </summary>
        public object Value { get; set; }

        #endregion

        // --------------------------------------------------
        // CONVERTERS
        // --------------------------------------------------

        #region Converters

        public static implicit operator BdoConstraint((string Reference, object Value, IBdoCondition Condition) item)
        {
            var constraint = BdoData.NewConstraintRequirement(item.Reference, item.Value, item.Condition);

            return constraint;
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoConstraint class.
        /// </summary>
        public BdoConstraint()
        {
            this.WithId();
        }

        #endregion

        // ------------------------------------------
        // IReferenced Implementation
        // ------------------------------------------

        #region IReferenced

        /// <summary>
        /// 
        /// </summary>
        public string Key() => Id;

        #endregion

        // ------------------------------------------
        // IIdentified Implementation
        // ------------------------------------------

        #region IIdentified

        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        #endregion

        // ------------------------------------------
        // IConditional Implementation
        // ------------------------------------------

        #region IConditional

        /// <summary>
        /// The condition.
        /// </summary>
        public IBdoCondition Condition { get; set; }

        #endregion
    }
}