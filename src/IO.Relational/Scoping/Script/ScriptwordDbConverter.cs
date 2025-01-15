using AutoMapper;
using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Meta;
using BindOpen.Data.Schema;
using BindOpen.Scoping.Script;
using System.Linq;

namespace BindOpen.Scoping.Script
{
    /// <summary>
    /// This class represents a Db converter of script words.
    /// </summary>
    public static class ScriptwordDbConverter
    {
        /// <summary>
        /// Converts a script word poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ScriptwordDb ToDb(
            this IBdoScriptword poco,
            DataDbContext context,
            bool root = true)
        {
            if (poco == null) return null;

            ScriptwordDb dbItem = new();

            if (root) poco = poco?.Root() as IBdoScriptword;

            dbItem.UpdateFromPoco(poco, context);

            return dbItem;
        }

        public static ScriptwordDb UpdateFromPoco(
            this ScriptwordDb dbItem,
            IBdoScriptword poco,
            DataDbContext context)
        {
            if (dbItem == null) return null;

            if (poco == null) return dbItem;

            MetaDataDbConverter.UpdateFromPoco<MetaDataDb>(dbItem, poco, context);
            dbItem.ValueType = DataValueTypes.Any;

            if (context == null)
            {
                dbItem.Child = poco.Child.ToDb(context, false);
            }
            else
            {
                dbItem.Child = context.Upsert(poco.Child);
            }

            return dbItem;
        }

        /// <summary>
        /// Converts a script word DTO into a poco one.
        /// </summary>
        /// <param key="dbItem">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static IBdoScriptword ToPoco(
            this ScriptwordDb dbItem)
        {
            if (dbItem == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<ScriptwordDb, BdoScriptword>()
                    .ForMember(q => q.Child, opt => opt.Ignore())
                    .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.Reference.ToPoco()))
                    .ForMember(q => q.DataType, opt => opt.Ignore())
                    .ForMember(q => q.Items, opt => opt.Ignore())
                    .ForMember(q => q.Parent, opt => opt.Ignore())
                    .ForMember(q => q.Reference, opt => opt.Ignore())
                    .ForMember(q => q.Schema, opt => opt.Ignore())
                );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoScriptword>(dbItem);

            poco.WithChild(dbItem.Child.ToPoco());

            poco.DataType = new BdoDataType(dbItem?.ClassReference?.ToPoco())
            {
                DefinitionUniqueName = dbItem.DefinitionUniqueName,
                Identifier = dbItem.Identifier,
                ValueType = DataValueTypes.Scriptword
            };
            poco.Schema = dbItem.Schema.ToPoco();
            poco.ExpressionKind = BdoExpressionKind.Word;

            poco.With(dbItem.MetaItems?.Select(q => q.ToPoco()).ToArray());

            return poco;
        }
    }
}
