using BindOpen.Framework.Core.Data.Expression;
using BindOpen.Framework.Databases.Extensions.Carriers;

namespace BindOpen.Framework.Databases.Data.Queries
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