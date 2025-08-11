using AutoMapper;
using BindOpen.Data.Meta;
using System.Linq;

namespace BindOpen.Data.Schema;

/// <summary>
/// This class represents a Db converter of schema sets.
/// </summary>
public static class SchemaSetDbConverter
{
    /// <summary>
    /// Converts a schema set poco into a DTO one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The DTO object.</returns>
    public static SchemaSetDb ToDb(
        this IBdoSchemaSet poco,
        DataDbContext context)
    {
        if (poco == null) return null;

        var config = new MapperConfiguration(
            cfg => cfg.CreateMap<BdoSchemaSet, SchemaSetDb>()
                .ForMember(q => q.Items, opt => opt.Ignore()),
            null
        );

        var mapper = new Mapper(config);
        var dbItem = mapper.Map<SchemaSetDb>(poco);

        dbItem.Items = poco.Items?.Select(q => q.ToDb(context)).ToList();

        return dbItem;
    }

    /// <summary>
    /// Converts a schema set DTO to a poco one.
    /// </summary>
    /// <param key="dbItem">The DTO to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoSchemaSet ToPoco(
        this SchemaSetDb dbItem)
    {
        if (dbItem == null) return null;

        var config = new MapperConfiguration(
            cfg => cfg.CreateMap<SchemaSetDb, BdoSchemaSet>()
                .ForMember(q => q.Items, opt => opt.Ignore()),
            null
        );

        var mapper = new Mapper(config);
        var poco = mapper.Map<BdoSchemaSet>(dbItem);

        poco.With(dbItem.Items?.Select(q => q.ToPoco()).ToArray());

        return poco;
    }
}
