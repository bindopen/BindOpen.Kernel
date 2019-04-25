using BindOpen.Framework.Core.Data.Expression;
using BindOpen.Framework.Databases.Extensions.Carriers;

namespace BindOpen.Framework.Databases.Data.Queries
{
    public interface IDbDataQueryJointureStatement
    {
        DataExpression Condition { get; set; }
        DbDataQueryJointureKind Kind { get; set; }
        DbTable Table { get; set; }
    }
}