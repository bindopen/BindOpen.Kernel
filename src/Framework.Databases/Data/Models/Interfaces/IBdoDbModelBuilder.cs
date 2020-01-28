using BindOpen.Framework.Data.Queries;
using BindOpen.Framework.Extensions.Carriers;

namespace BindOpen.Framework.Data.Models
{
    public interface IBdoDbModelBuilder
    {
        IBdoDbModelBuilder AddJoinCondition(DbQueryJoinCondition condition, string name = null);

        IBdoDbModelBuilder AddTable(DbTable table, string name = null);

        IBdoDbModelBuilder AddTable<T>() where T : class;

        IBdoDbModelBuilder AddTuple(DbField[] fields, string name);

        IBdoDbModelBuilder AddQuery(IDbQuery query, string name = null);
    }
}