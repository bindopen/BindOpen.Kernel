using BindOpen.Framework.Data.Expression;
using BindOpen.Framework.Extensions.Carriers;

namespace BindOpen.Framework.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryJointureStatement
    {
        /// <summary>
        /// 
        /// </summary>
        DataExpression Condition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DbQueryJointureKind Kind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DbTable Table { get; set; }
    }
}