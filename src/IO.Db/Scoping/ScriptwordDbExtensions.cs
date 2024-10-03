using BindOpen.Data;
using BindOpen.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BindOpen.Scoping.Script;

/// <summary>
/// This static class provides methods to create extension items.
/// </summary>
public static class ScriptwordDbExtensions
{
    public static ScriptwordDto GetScriptword(
        this DataDbContext context,
        string identifier)
    {
        return context.Scriptwords
            .Include(q => q.Child)
            .Include(q => q.ClassReference)
            .Include(q => q.Expression)
            .Include(q => q.Spec)
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    public static ScriptwordDto Upsert(
        this DataDbContext context,
        IBdoScriptword poco)
    {
        if (context == null || poco == null) return default;

        poco.Identifier ??= StringHelper.NewGuid();

        var dbItem = context.GetScriptword(poco.Identifier);

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
