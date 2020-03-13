using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Extensions.Carriers;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryCondition : IDbQueryItem
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

        /// <summary>
        /// The set of parameters of this instance.
        /// </summary>
        DataElementSet ParameterSet { get; set; }

        #endregion
    }
}