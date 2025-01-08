using AutoMapper;
using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Meta;
using BindOpen.Scoping.Script;
using System.Linq;

namespace BindOpen.Scoping.Script
{
    /// <summary>
    /// This class represents a IO converter of script words.
    /// </summary>
    public static class ScriptwordIOConverter
    {
        /// <summary>
        /// Converts a script word poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ScriptwordDto ToDto(this IBdoScriptword poco, bool root = true)
        {
            ScriptwordDto dto = new();

            if (root) poco = poco?.Root() as IBdoScriptword;

            dto.UpdateFromPoco(poco);

            return dto;
        }

        public static ScriptwordDto UpdateFromPoco(
            this ScriptwordDto dto,
            IBdoScriptword poco)
        {
            if (dto == null) return null;

            if (poco == null) return dto;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<IBdoScriptword, ScriptwordDto>()
                    .ForMember(q => q.Child, opt => opt.MapFrom(q => q.Child.ToDto(false)))
                    .ForMember(q => q.ClassReference, opt => opt.Ignore())
                    .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.Reference.ToDto()))
                    //.ForMember(q => q.Item, opt => opt.Ignore())
                    .ForMember(q => q.MetaItems, opt => opt.Ignore())
                    .ForMember(q => q.Spec, opt => opt.MapFrom(q => q.Spec.ToDto()))
                );

            var mapper = new Mapper(config);
            mapper.Map(poco, dto);

            dto.ClassReference = poco.DataType.IsSpecified() ? poco?.DataType.ToDto() : null;
            dto.DefinitionUniqueName = poco?.DataType?.DefinitionUniqueName;

            dto.MetaItems = poco.Items?.Select(q => q.ToDto()).ToList();
            dto.ValueType = DataValueTypes.Any;

            return dto;
        }

        /// <summary>
        /// Converts a script word DTO into a poco one.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static IBdoScriptword ToPoco(
            this ScriptwordDto dto)
        {
            if (dto == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<ScriptwordDto, BdoScriptword>()
                    .ForMember(q => q.Child, opt => opt.Ignore())
                    .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.Reference.ToPoco()))
                    .ForMember(q => q.DataType, opt => opt.Ignore())
                    .ForMember(q => q.Items, opt => opt.Ignore())
                    .ForMember(q => q.Parent, opt => opt.Ignore())
                    .ForMember(q => q.Reference, opt => opt.Ignore())
                    .ForMember(q => q.Spec, opt => opt.Ignore())
                );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoScriptword>(dto);

            poco.WithChild(dto.Child.ToPoco());

            poco.DataType = new BdoDataType(dto?.ClassReference?.ToPoco())
            {
                DefinitionUniqueName = dto.DefinitionUniqueName,
                Identifier = dto.Identifier,
                ValueType = DataValueTypes.Scriptword
            };
            poco.Spec = dto.Spec.ToPoco();
            poco.ExpressionKind = BdoExpressionKind.Word;

            poco.With(dto.MetaItems?.Select(q => q.ToPoco()).ToArray());

            return poco;
        }
    }
}
