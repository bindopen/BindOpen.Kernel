using BindOpen.Data.Helpers;

namespace BindOpen.Data.Meta;

/// <summary>
/// This static class provides methods to create extension items.
/// </summary>
public static class MetaDataDbExtensions
{

    public static IBdoMetaData Repair(IBdoMetaData poco)
    {
        if (poco != null)
        {
            poco.Identifier ??= StringHelper.NewGuid();

            poco.DataType.Identifier = poco.Identifier;
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
