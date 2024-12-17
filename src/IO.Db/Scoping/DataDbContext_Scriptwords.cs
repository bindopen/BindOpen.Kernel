using BindOpen.Data;
using BindOpen.Data.Helpers;
using BindOpen.Scoping.Script;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BindOpen.Data;

public partial class DataDbContext : DbContext
{
    public ScriptwordDto GetScriptword(string identifier)
    {
        return Scriptwords
            .Include(q => q.Child)
            .Include(q => q.ClassReference)
            .Include(q => q.Expression)
            .Include(q => q.Spec)
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    public ScriptwordDto Upsert(IBdoScriptword poco)
    {
        if (poco == null) return default;

        poco.Identifier ??= StringHelper.NewGuid();

        var dbItem = GetScriptword(poco.Identifier);

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
