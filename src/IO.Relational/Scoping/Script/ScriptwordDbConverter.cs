using AutoMapper;
using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Meta;
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
        public static ScriptwordDb ToDb(this IBdoScriptword poco, bool root = true)
        {
            ScriptwordDb dbItem = new();

            if (root) poco = poco?.Root() as IBdoScriptword;

            dbItem.UpdateFromPoco(poco);

            return dbItem;
        }

        public static ScriptwordDb UpdateFromPoco(
            this ScriptwordDb dbItem,
            IBdoScriptword poco)
        {
            if (dbItem == null) return null;

            if (poco == null) return dbItem;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<IBdoScriptword, ScriptwordDb>()
                    .ForMember(q => q.Child, opt => opt.MapFrom(q => q.Child.ToDb(false)))
                    .ForMember(q => q.ClassReference, opt => opt.Ignore())
                    .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.Reference.ToDb()))
                    //.ForMember(q => q.Item, opt => opt.Ignore())
                    .ForMember(q => q.MetaItems, opt => opt.Ignore())
                    .ForMember(q => q.Spec, opt => opt.MapFrom(q => q.Spec.ToDb()))
                );

            var mapper = new Mapper(config);
            mapper.Map(poco, dbItem);

            dbItem.ClassReference = poco.DataType.IsSpecified() ? poco?.DataType.ToDb() : null;
            dbItem.DefinitionUniqueName = poco?.DataType?.DefinitionUniqueName;

            dbItem.MetaItems = poco.Items?.Select(q => q.ToDb()).ToList();
            dbItem.ValueType = DataValueTypes.Any;

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
                    .ForMember(q => q.Spec, opt => opt.Ignore())
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
            poco.Spec = dbItem.Spec.ToPoco();
            poco.ExpressionKind = BdoExpressionKind.Word;

            poco.With(dbItem.MetaItems?.Select(q => q.ToPoco()).ToArray());

            return poco;
        }
    }
}
