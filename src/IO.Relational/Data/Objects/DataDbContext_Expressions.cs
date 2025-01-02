using BindOpen.Data.Helpers;
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

    public IBdoExpression Repair(IBdoExpression poco)
    {
        if (poco != null)
        {
            poco.Identifier ??= StringHelper.NewGuid();
        }

        return poco;
    }

    public ExpressionDb Upsert(IBdoExpression poco)
    {
        if (poco == null) return default;

        Repair(poco);

        var dbItemItem = GetExpression(poco.Identifier);

        if (dbItemItem == null)
        {
            var dbItem = poco.ToDb();
            Add(dbItem);
        }
        else
        {
            dbItemItem.UpdateFromPoco(poco);
        }

        return dbItemItem;
    }
}
