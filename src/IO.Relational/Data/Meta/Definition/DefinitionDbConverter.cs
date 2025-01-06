using AutoMapper;
using BindOpen.Data.Helpers;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a Db converter of definitions.
    /// </summary>
    public static class DefinitionDbConverter
    {
        /// <summary>
        /// Converts a definition poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static DefinitionDb ToDb(
            this IBdoDefinition poco,
            DataDbContext context) => poco.ToDb<DefinitionDb>(context);

        /// <summary>
        /// Converts a definition poco of the specified class into a DTO one.
        /// </summary>
        /// <typeparam name="T">The type of configuration to consider.</typeparam>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static T ToDb<T>(
            this IBdoDefinition poco,
            DataDbContext context)
            where T : DefinitionDb
        {
            if (poco == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<IBdoDefinition, T>()
                    .ForMember(q => q.CreationDate, opt => opt.MapFrom(q => StringHelper.ToString(q.CreationDate)))
                    .ForMember(q => q.Description, opt => opt.MapFrom(q => q.Description.ToDb(context)))
                    .ForMember(q => q.Items, opt => opt.MapFrom(q => q.Items == null ? null : q.Items.Select(q => q.ToDb(context)).ToList()))
                    .ForMember(q => q.LastModificationDate, opt => opt.MapFrom(q => StringHelper.ToString(q.CreationDate)))
                    .ForMember(q => q.Title, opt => opt.MapFrom(q => q.Title.ToDb(context)))
                    .ForMember(q => q.UsedItemIds, opt => opt.MapFrom(q => q.UsedItemIds == null ? null : q.UsedItemIds.Select(q => q).ToList()))
            );

            var mapper = new Mapper(config);
            var dbItem = mapper.Map<T>(poco);

            return dbItem;
        }

        /// <summary>
        /// Converts a definition DTO to a poco one.
        /// </summary>
        /// <param key="dbItem">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static IBdoDefinition ToPoco(
            this DefinitionDb dbItem) => dbItem.ToPoco<BdoDefinition>();

        /// <summary>
        /// Converts a definition DTO of the specified class to a poco one.
        /// </summary>
        /// <typeparam name="T">The type of configuration to consider.</typeparam>
        /// <param key="dbItem">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static T ToPoco<T>(
            this DefinitionDb dbItem)
            where T : IBdoDefinition
        {
            if (dbItem == null) return default;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<DefinitionDb, T>()
                    .ForMember(q => q.CreationDate, opt => opt.MapFrom(q => q.CreationDate))
                    .ForMember(q => q.Description, opt => opt.Ignore())
                    .ForMember(q => q.Items, opt => opt.Ignore())
                    .ForMember(q => q.LastModificationDate, opt => opt.MapFrom(q => q.LastModificationDate))
                    .ForMember(q => q.Title, opt => opt.Ignore())
                    .ForMember(q => q.UsedItemIds, opt => opt.MapFrom(q => q.UsedItemIds == null ? null : q.UsedItemIds.Select(q => q).ToList()))
            );

            var mapper = new Mapper(config);
            var poco = mapper.Map<T>(dbItem);
            poco
                .WithTitle(dbItem.Title.ToPoco<string>())
                .WithDescription(dbItem.Description.ToPoco<string>())
                .With(dbItem.Items.Select(q => q.ToPoco()).ToArray());

            return poco;
        }
    }
}
