using BindOpen.Framework.Core.Data.Expression;

namespace BindOpen.Framework.Databases.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryHavingStatement
    {
        /// <summary>
        /// 
        /// </summary>
        IDataExpression DataExpression { get; set; }
    }
}