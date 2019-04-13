using BindOpen.Framework.Core.Data.Expression;

namespace BindOpen.Framework.Databases.Data.Queries
{
    public interface IDbDataQueryHavingStatement
    {
        IDataExpression DataExpression { get; set; }
    }
}