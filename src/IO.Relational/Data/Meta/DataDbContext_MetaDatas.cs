using BindOpen.Data.Meta;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BindOpen.Data;

public partial class DataDbContext : DbContext
{
    public MetaDataDb GetMetaData(string identifier)
    {
        return MetaDatas
            .Include(q => q.ClassReference)
            .Include(q => q.Reference)
            .Include(q => q.Spec)
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    public MetaDataDb Upsert(IBdoMetaData poco)
    {
        if (poco == null) return default;

        var dbItem = GetMetaData(poco.Identifier);

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

    public IBdoMetaData Delete(IBdoMetaData poco)
    {
        if (poco == null) return null;

        var dbItem = GetMetaData(poco.Identifier);

        if (dbItem != null)
        {
            Remove(dbItem);
        }

        return poco;
    }
    //public static MetaDataDb GetMetaData(
    //    this DataDbContext context,
    //    string identifier)
    //{
    //    return context.MetaDatas
    //        .Include(q => q.ClassReference)
    //        .Include(q => q.Reference)
    //        .Include(q => q.Spec)
    //        .Include(q => q.Supers)
    //        .FirstOrDefault(q => q.Identifier == identifier);
    //}

    //public static MetaDataDb Upsert(
    //    this DataDbContext context,
    //    IBdoMetaData poco)
    //{
    //    if (context == null || poco?.Name == null) return default;

    //    var dbItem = context.GetMetaData(poco.Name);

    //    if (dbItem == null)
    //    {
    //        var dbItem = poco.ToDb(this);
    //        context.Add(dbItem);
    //    }
    //    else
    //    {
    //        dbItem.UpdateFromPoco(poco, this);
    //    }

    //    return dbItem;
    //}
}
