using AutoMapper;
using BindOpen.Data.Helpers;

namespace BindOpen.Data.Assemblies;

/// <summary>
/// This class represents a Db converter of assembly references.
/// </summary>
public static class ClassReferenceDbConverter
{
    /// <summary>
    /// Converts an assembly reference poco into a database entity one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The database entity object.</returns>
    public static ClassReferenceDb ToDb(
        this IBdoClassReference poco)
    {
        if (poco == null) return null;

        ClassReferenceDb dbItem = new();
        dbItem.UpdateFromPoco(poco);

        return dbItem;
    }

    public static ClassReferenceDb UpdateFromPoco(
        this ClassReferenceDb dbItem,
        IBdoClassReference poco)
    {
        if (dbItem == null) return null;

        if (poco == null) return dbItem;

        poco.Identifier ??= StringHelper.NewGuid();

        var config = new MapperConfiguration(
            cfg => cfg.CreateMap<BdoClassReference, ClassReferenceDb>()
        );

        var mapper = new Mapper(config);
        mapper.Map(poco, dbItem);

        return dbItem;
    }

    /// <summary>
    /// Converts an assembly reference database entity into a poco one.
    /// </summary>
    /// <param key="dbItem">The database entity to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoClassReference ToPoco(
        this ClassReferenceDb dbItem)
    {
        if (dbItem == null) return null;

        var config = new MapperConfiguration(
            cfg => cfg.CreateMap<ClassReferenceDb, BdoClassReference>()
        );

        var mapper = new Mapper(config);
        var poco = mapper.Map<BdoClassReference>(dbItem);

        return poco;
    }
}
