using BindOpen.Data.Expression;

namespace BindOpen.Data.Queries
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