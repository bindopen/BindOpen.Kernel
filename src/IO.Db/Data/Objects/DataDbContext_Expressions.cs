using BindOpen.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BindOpen.Data;

public partial class DataDbContext : DbContext
{
    public ExpressionDto GetExpression(string identifier)
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

    public ExpressionDto Upsert(IBdoExpression poco)
    {
        if (poco == null) return default;

        Repair(poco);

        var dbItem = GetExpression(poco.Identifier);

        if (dbItem == null)
        {
            var dto = poco.ToDto();
            Add(dto);
        }
        else
        {
            dbItem.UpdateFromPoco(poco);
        }

        return dbItem;
    }
}
