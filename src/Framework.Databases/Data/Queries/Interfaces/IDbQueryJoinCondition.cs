using BindOpen.Framework.Data.Common;
using BindOpen.Framework.Extensions.Carriers;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryJoinCondition
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The field 1 of this instance.
        /// </summary>
        DbField Field1 { get; set; }

        /// <summary>
        /// The field 2 of this instance.
        /// </summary>
        DbField Field2 { get; set; }

        /// <summary>
        /// The operator of this instance.
        /// </summary>
        DataOperator Operator { get; set; }

        #endregion
    }
}