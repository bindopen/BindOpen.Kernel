using BindOpen.Kernel.Data.Conditions;
using System.Data;

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
        /// The group identifier.
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// The result code.
        /// </summary>
        public string ResultCode { get; set; }

        /// <summary>
        /// The value.
        /// </summary>
        public object Value { get; set; }

        #endregion

        // --------------------------------------------------
        // CONVERTERS
        // --------------------------------------------------

        #region Converters

        public static implicit operator BdoConstraint((object Value, IBdoCondition Condition) item)
        {
            var constraint = BdoData.NewConstraint(item.Value, item.Condition);

            return constraint;
        }

        public static implicit operator BdoConstraint((object Value, IBdoCondition Condition, string GroupId) item)
        {
            var constraint = BdoData.NewConstraint(item.Value, item.Condition, item.GroupId);

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