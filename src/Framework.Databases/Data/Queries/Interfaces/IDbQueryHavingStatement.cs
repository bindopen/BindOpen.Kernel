using BindOpen.Framework.Data.Expression;

namespace BindOpen.Framework.Data.Queries
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