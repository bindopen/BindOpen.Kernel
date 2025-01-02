using BindOpen.Data;
using BindOpen.Data.Helpers;
using BindOpen.Scoping.Script;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BindOpen.Data;

public partial class DataDbContext : DbContext
{
    public ScriptwordDb GetScriptword(string identifier)
    {
        return Scriptwords
            .Include(q => q.Child)
            .Include(q => q.ClassReference)
            .Include(q => q.Expression)
            .Include(q => q.Spec)
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    public ScriptwordDb Upsert(IBdoScriptword poco)
    {
        if (poco == null) return default;

        poco.Identifier ??= StringHelper.NewGuid();

        var dbItemItem = GetScriptword(poco.Identifier);

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
