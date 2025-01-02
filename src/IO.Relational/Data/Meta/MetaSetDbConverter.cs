using AutoMapper;
using BindOpen.Data.Meta;
using BindOpen.Data.Meta.Reflection;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a Db converter of meta sets.
    /// </summary>
    public static class MetaSetDbConverter
    {
        /// <summary>
        /// Converts an expression poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaSetDb ToDb(this IBdoMetaSet poco)
        {
            MetaSetDb dbItem = new();
            dbItem.UpdateFromPoco(poco);

            return dbItem;
        }

        public static MetaSetDb UpdateFromPoco(
            this MetaSetDb dbItem,
            IBdoMetaSet poco)
        {
            if (dbItem == null) return null;

            if (poco == null) return dbItem;

            poco.UpdateTrees();

            MapperConfiguration config;

            config = new MapperConfiguration(
                cfg => cfg.CreateMap<IBdoMetaSet, MetaSetDb>()
                    .ForMember(q => q.Items, opt => opt.Ignore())
            );

            var mapper = new Mapper(config);
            mapper.Map(poco, dbItem);

            dbItem.Items = poco.Items?.Select(q => q.ToDb()).ToList();

            return dbItem;
        }

        /// <summary>
        /// Converts a meta set DTO to a poco one.
        /// </summary>
        /// <param key="dbItem">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static IBdoMetaSet ToPoco(
            this MetaSetDb dbItem)
        {
            if (dbItem == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<MetaSetDb, BdoMetaSet>()
                    .ForMember(q => q.Items, opt => opt.Ignore())
                );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoMetaSet>(dbItem);

            poco.With(dbItem.Items?.Select(q => q.ToPoco()).ToArray());

            return poco;
        }
    }
}
