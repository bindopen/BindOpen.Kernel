﻿using BindOpen.Data;
using BindOpen.Scoping.Script;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BindOpen.Data;

public partial class DataDbContext : DbContext
{
    public ScriptwordDb GetScriptword(string identifier)
    {
        var scriptword = Scriptwords
            .Include(q => q.Child)
            .Include(q => q.MetaItems)
            .Include(q => q.ClassReference)
            .Include(q => q.Expression)
            .Include(q => q.Schema)
            .FirstOrDefault(q => q.Identifier == identifier);

        scriptword?.UpdateTree();

        return scriptword;
    }

    public ScriptwordDb Upsert(IBdoScriptword poco)
    {
        if (poco == null) return default;

        var dbItem = GetScriptword(poco.Identifier);

        if (dbItem == null)
        {
            dbItem = ScriptwordDbConverter.ToDb(poco, this);
            Add(dbItem);
        }
        else
        {
            dbItem.UpdateFromPoco(poco, this);
        }

        return dbItem;
    }
}
