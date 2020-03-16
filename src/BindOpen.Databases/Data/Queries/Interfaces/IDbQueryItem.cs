using BindOpen.Data.Expression;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbQueryItem
    {
        /// <summary>
        /// The value to consider.
        /// </summary>
        DataExpression Expression { get; set; }
    }
}