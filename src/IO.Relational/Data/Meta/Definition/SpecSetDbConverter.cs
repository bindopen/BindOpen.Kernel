using AutoMapper;
using BindOpen.Data.Meta;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a Db converter of specification sets.
    /// </summary>
    public static class SpecSetDbConverter
    {
        /// <summary>
        /// Converts a specification set poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static SpecSetDb ToDb(this IBdoSpecSet poco)
        {
            if (poco == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<BdoSpecSet, SpecSetDb>()
                    .ForMember(q => q.Items, opt => opt.Ignore())
            );

            var mapper = new Mapper(config);
            var dbItem = mapper.Map<SpecSetDb>(poco);

            dbItem.Items = poco.Items?.Select(q => q.ToDb()).ToList();

            return dbItem;
        }

        /// <summary>
        /// Converts a specification set DTO to a poco one.
        /// </summary>
        /// <param key="dbItem">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static IBdoSpecSet ToPoco(
            this SpecSetDb dbItem)
        {
            if (dbItem == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<SpecSetDb, BdoSpecSet>()
                    .ForMember(q => q.Items, opt => opt.Ignore())
                );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoSpecSet>(dbItem);

            poco.With(dbItem.Items?.Select(q => q.ToPoco()).ToArray());

            return poco;
        }
    }
}
