﻿using AutoMapper;
using BindOpen.System.Data.Assemblies;
using BindOpen.System.Data.Helpers;
using System.Linq;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class MetaScalarConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaScalarDto ToDto(this IBdoMetaScalar poco)
        {
            if (poco == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<BdoMetaScalar, MetaScalarDto>()
                    .ForMember(q => q.ClassReference, opt => opt.Ignore())
                    .ForMember(q => q.Item, opt => opt.Ignore())
                    .ForMember(q => q.DataReference, opt => opt.MapFrom(q => q.DataReference.ToDto()))
                    .ForMember(q => q.Specs, opt => opt.Ignore())
            );

            var mapper = new Mapper(config);
            var dto = mapper.Map<MetaScalarDto>(poco);

            dto.ClassReference = poco?.DataType.ClassReference?.ToDto();
            dto.DataReference = poco.DataReference?.ToDto();

            var dataList = poco.GetDataList<object>()?.Select(q => q.ToString(dto.ValueType)).ToList();
            if (dataList?.Count > 1)
            {
                dto.Items = dataList;
            }
            else
            {
                dto.Item = dataList?.FirstOrDefault();
            }

            dto.Specs = poco.Specs?.Select(q =>
            {
                var dto = q.ToDto();
                if (q.DataType.ValueType == poco.DataType.ValueType)
                {
                    dto.ValueType = DataValueTypes.Any;
                }
                return dto;
            }).ToList();
            dto.ValueType = poco?.DataType.ValueType ?? DataValueTypes.Any;

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoMetaScalar ToPoco(
            this MetaScalarDto dto)
        {
            if (dto == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<MetaScalarDto, BdoMetaScalar>()
                    .ForMember(q => q.DataType, opt => opt.Ignore())
                    .ForMember(q => q.Parent, opt => opt.Ignore())
                    .ForMember(q => q.DataReference, opt => opt.Ignore())
                    .ForMember(q => q.Specs, opt => opt.Ignore())
                );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoMetaScalar>(dto);

            poco.DataReference = dto.DataReference.ToPoco();
            poco.DataType = new BdoDataType()
            {
                ClassReference = dto.ClassReference.ToPoco(),
                ValueType = dto.ValueType
            };
            var specs = dto.Specs?.Select(q => q.ToPoco())?.ToArray();
            poco.Specs = specs?.Length > 0 ? BdoData.NewSet<IBdoSpec>(specs) : null;

            if (!string.IsNullOrEmpty(dto.Item))
            {
                poco.WithData(dto.Item.ToObject(poco.DataType.ValueType));
            }
            else
            {
                var objects = dto.Items?.Select(q => q.ToObject(poco.DataType.ValueType)).ToList();
                poco.WithData(objects);
            }

            return poco;
        }
    }
}
