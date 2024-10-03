using BindOpen.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BindOpen.Data;

/// <summary>
/// This static class provides methods to create extension items.
/// </summary>
public static class ExpressionDbExtensions
{
    public static ExpressionDto GetExpression(
        this DataDbContext context,
        string identifier)
    {
        return context.Expressions
            .Include(q => q.Scriptword)
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    private static IBdoExpression Repair(IBdoExpression poco)
    {
        poco.Identifier ??= StringHelper.NewGuid();

        return poco;
    }

    public static ExpressionDto Upsert(
        this DataDbContext context,
        IBdoExpression poco)
    {
        if (context == null || poco == null) return default;

        Repair(poco);

        var dbItem = context.GetExpression(poco.Identifier);

        if (dbItem == null)
        {
            var dto = poco.ToDto();
            context.Add(dto);
        }
        else
        {
            dbItem.UpdateFromPoco(poco);
        }

        return dbItem;
    }
}
