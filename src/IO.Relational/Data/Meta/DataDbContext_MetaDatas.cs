using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BindOpen.Data;

public partial class DataDbContext : DbContext
{

    public IBdoMetaData Repair(IBdoMetaData poco)
    {
        if (poco != null)
        {
            poco.Identifier ??= StringHelper.NewGuid();

            poco.DataType.Identifier = poco.Identifier;
        }

        return poco;
    }

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

        Repair(poco);

        var dbItemItem = GetMetaData(poco.Identifier);

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

    //    var dbItemItem = context.GetMetaData(poco.Name);

    //    if (dbItemItem == null)
    //    {
    //        var dbItem = poco.ToDb();
    //        context.Add(dbItem);
    //    }
    //    else
    //    {
    //        dbItemItem.UpdateFromPoco(poco);
    //    }

    //    return dbItemItem;
    //}
}
