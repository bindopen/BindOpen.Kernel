using AutoMapper;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a IO converter of configurations.
    /// </summary>
    public static class ConfigurationDbConverter
    {
        /// <summary>
        /// Converts a configuration poco of the specified class into a DTO one.
        /// </summary>
        /// <typeparam name="T">The type of configuration to consider.</typeparam>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ConfigurationDb ToDb(
            this IBdoConfiguration poco,
            DataDbContext context)
        {
            if (poco == null) return null;

            ConfigurationDb db = new();
            db.UpdateFromPoco(poco, context);

            return db;
        }

        public static ConfigurationDb UpdateFromPoco(
            this ConfigurationDb dbItem,
            IBdoConfiguration poco,
            DataDbContext context)
        {
            if (poco == null) return dbItem;

            (dbItem as MetaSetDb).UpdateFromPoco(poco, context);

            dbItem.CreationDate = poco.CreationDate;
            dbItem.LastModificationDate = poco.LastModificationDate;
            dbItem.UsedItemIds = poco.UsedItemIds?.ToList();

            if (context != null)
            {
                dbItem.Children ??= [];
                dbItem.Children.RemoveAll(q => poco._Children?.Any(p => p?.Identifier == q?.Identifier) != true);

                if (poco?._Children?.Count > 0)
                {
                    foreach (var subItem in poco._Children)
                    {
                        context.Upsert(subItem);
                    }
                }

                dbItem.Description = context.Upsert(poco.Description);
                dbItem.Title = context.Upsert(poco.Title);
            }

            return dbItem;
        }

        /// <summary>
        /// Converts a configuration DTO of the specified class to a poco one.
        /// </summary>
        /// <typeparam name="T">The type of configuration DTO to consider.</typeparam>
        /// <param key="db">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static IBdoConfiguration ToPoco(this ConfigurationDb db)
        {
            if (db == null) return default;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<ConfigurationDb, BdoConfiguration>()
                    .ForMember(q => q.CreationDate, opt => opt.MapFrom(q => q.CreationDate))
                    .ForMember(q => q.Description, opt => opt.Ignore())
                    .ForMember(q => q.Items, opt => opt.Ignore())
                    .ForMember(q => q.LastModificationDate, opt => opt.MapFrom(q => q.CreationDate))
                    .ForMember(q => q.Title, opt => opt.Ignore())
                    .ForMember(q => q.UsedItemIds, opt => opt.MapFrom(q => q.UsedItemIds == null ? null : q.UsedItemIds.Select(q => q).ToList()))
            );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoConfiguration>(db);
            poco
                .WithTitle(db.Title.ToPoco<string>())
                .WithDescription(db.Description.ToPoco<string>())
                .With(db.Items.Select(q => q.ToPoco()).ToArray())
                .WithChildren(db.Children?.Select(q => q.ToPoco()).ToArray());

            return poco;
        }
    }
}
