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
            .Include(q => q.Word)
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    public static ExpressionDto Upsert(
        this DataDbContext context,
        IBdoExpression poco)
    {
        if (context == null || poco?.Identifier == null) return default;

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
