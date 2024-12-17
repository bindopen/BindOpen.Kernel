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

    public MetaDataDto GetMetaData(string identifier)
    {
        return MetaDatas
            .Include(q => q.MetaNode)
            .Include(q => q.MetaObject)
            .Include(q => q.MetaScalar)
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    public MetaDataDto Upsert(IBdoMetaData poco)
    {
        if (poco == null) return default;

        Repair(poco);

        var dbItem = GetMetaData(poco.Identifier);

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

    public IBdoMetaData Delete(IBdoMetaData poco)
    {
        if (poco == null) return null;

        var dto = GetMetaData(poco.Identifier);

        if (dto != null)
        {
            if (dto.MetaNode is not null)
            {
                Remove(dto.MetaNode);
            }
            else if (dto.MetaObject is not null)
            {
                Remove(dto.MetaObject);
            }
            else if (dto.MetaScalar is not null)
            {
                Remove(dto.MetaScalar);
            }
        }

        return poco;
    }
    //public static MetaDataDto GetMetaData(
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

    //public static MetaDataDto Upsert(
    //    this DataDbContext context,
    //    IBdoMetaData poco)
    //{
    //    if (context == null || poco?.Name == null) return default;

    //    var dbItem = context.GetMetaData(poco.Name);

    //    if (dbItem == null)
    //    {
    //        var dto = poco.ToDto();
    //        context.Add(dto);
    //    }
    //    else
    //    {
    //        dbItem.UpdateFromPoco(poco);
    //    }

    //    return dbItem;
    //}
}
