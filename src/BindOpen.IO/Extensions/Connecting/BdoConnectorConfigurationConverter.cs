//using AutoMapper;
//using BindOpen.Data;
//using BindOpen.Data.Configuration;
//using BindOpen.Data.Helpers;
//using BindOpen.Data.Meta;
//using BindOpen.Data.Meta.Reflection;
//using BindOpen.Extensions.Scripting;
//using System.Linq;

//namespace BindOpen.Extensions.Connecting
//{
//    /// <summary>
//    /// This class represents a connector converter.
//    /// </summary>
//    public static class BdoConnectorConfigurationConverter
//    {
//        /// <summary>
//        /// Converts to DTO.
//        /// </summary>
//        /// <param key="poco">The poco to consider.</param>
//        /// <returns>The DTO object.</returns>
//        public static ConfigurationDto ToDto(this IBdoConnector poco)
//        {
//            if (poco == null) return null;

//            var items = poco
//                .ToMetaData(null, true)
//                .AsMetaSet()?.Select(p => p.ToDto()).ToList();

//            var config = new MapperConfiguration(
//                cfg => cfg.CreateMap<IBdoConnector, ConfigurationDto>()
//                    .ForMember(q => q.CreationDate, opt => opt.MapFrom(q => StringHelper.ToString(q.CreationDate)))
//                    .ForMember(q => q.DataExpression, opt => opt.MapFrom(q => q.DataExpression.ToDto()))
//                    .ForMember(q => q.Description, opt => opt.MapFrom(q => q.Description.ToDto()))
//                    .ForMember(q => q.MetaItems, opt => opt.MapFrom(q => q.Items == null ? null : q.Items.Select(q => q.ToDto()).ToList()))
//                    .ForMember(q => q.LastModificationDate, opt => opt.MapFrom(q => StringHelper.ToString(q.CreationDate)))
//                    .ForMember(q => q.Title, opt => opt.Ignore())
//                    .ForMember(q => q.UsedItemIds, opt => opt.MapFrom(q => q.UsedItemIds == null ? null : q.UsedItemIds.Select(q => q).ToList()))

//                    .ForMember(q => q.DataExpression, opt => opt.Ignore())
//                    .ForMember(q => q.DefinitionUniqueName, opt => opt.MapFrom(q => q.DefinitionUniqueName))
//                    .ForMember(q => q.Description, opt => opt.Ignore())
//                    .ForMember(q => q.ExtensionKind, opt => opt.MapFrom(q => BdoExtensionKind.Connector))
//                    .ForMember(q => q.MetaItems, opt => opt.MapFrom(q => items))
//                    .ForMember(q => q.UsedItemIds, opt => opt.Ignore())
//                );

//            var mapper = new Mapper(config);
//            var dto = mapper.Map<ConfigurationDto>(poco);

//            return dto;
//        }

//        /// <summary>
//        /// Converts to DTO.
//        /// </summary>
//        /// <param key="dto">The DTO to consider.</param>
//        /// <returns>The DTO object.</returns>
//        public static IBdoConnector ToConnectorPoco(
//            this ConfigurationDto dto)
//        {
//            if (dto == null) return null;

//            var metas = dto.MetaItems.Select(p => p.ToPoco()).ToArray();
//            var poco = BdoConfig.New(metas);

//            return poco;
//        }
//    }
//}
