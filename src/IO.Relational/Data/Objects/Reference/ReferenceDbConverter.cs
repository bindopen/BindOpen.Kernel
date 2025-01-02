using AutoMapper;
using BindOpen.Data.Meta;

namespace BindOpen.Data;

/// <summary>
/// This class represents a Db converter of references.
/// </summary>
public static class ReferenceDbConverter
{
    /// <summary>
    /// Converts a reference poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static ReferenceDb ToDb(this IBdoReference poco)
    {
        ReferenceDb dbItem = new();
        dbItem.UpdateFromPoco(poco);

        return dbItem;
    }

    public static ReferenceDb UpdateFromPoco(
        this ReferenceDb dbItem,
        IBdoReference poco)
    {
        if (dbItem == null) return null;

        if (poco == null) return dbItem;

        var config = new MapperConfiguration(
            cfg => cfg.CreateMap<BdoReference, ReferenceDb>()
                .ForMember(q => q.Expression, opt => opt.Ignore())
                .ForMember(q => q.MetaData, opt => opt.Ignore())
        );

        var mapper = new Mapper(config);
        mapper.Map(poco, dbItem);

        // Expression

        if (dbItem.Expression?.Identifier != poco?.Identifier)
        {
            dbItem.Expression = poco.Expression.ToDb();
        }
        else if (poco.Expression != null)
        {
            dbItem.Expression ??= new();
            dbItem.Expression.UpdateFromPoco(poco.Expression);
        }

        return dbItem;
    }

    /// <summary>
    /// Converts a reference DTO to a poco one.
    /// </summary>
    /// <param key="dbItem">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoReference ToPoco(
        this ReferenceDb dbItem)
    {
        if (dbItem == null) return null;

        var config = new MapperConfiguration(
            cfg => cfg.CreateMap<ReferenceDb, BdoReference>()
                .ForMember(q => q.Expression, opt => opt.MapFrom(q => q.Expression.ToPoco()))
                .ForMember(q => q.MetaData, opt => opt.MapFrom(q => q.MetaData.ToPoco()))
        );

        var mapper = new Mapper(config);
        var poco = mapper.Map<BdoReference>(dbItem);

        return poco;
    }
}
