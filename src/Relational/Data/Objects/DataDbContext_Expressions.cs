using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BindOpen.Data;

public partial class DataDbContext : DbContext
{
    public ExpressionDb GetExpression(string identifier)
    {
        return Expressions
            .Include(q => q.Scriptword)
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    public ExpressionDb Upsert(IBdoExpression poco)
    {
        if (poco == null) return default;

        var dbItem = GetExpression(poco.Identifier);

        if (dbItem == null)
        {
            dbItem = poco.ToDb(this);
            Add(dbItem);
        }
        else
        {
            dbItem.UpdateFromPoco(poco, this);
        }

        return dbItem;
    }

    public ExpressionDb Delete(IBdoExpression poco)
    {
        if (poco == null) return null;

        var dbItem = GetExpression(poco.Identifier);

        if (dbItem != null)
        {
            Remove(dbItem);
        }

        return dbItem;
    }
}
