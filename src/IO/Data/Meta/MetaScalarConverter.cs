﻿using AutoMapper;
using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Helpers;
using BindOpen.Data.Schema;
using BindOpen.Scoping.Script;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a IO converter of meta scalars.
    /// </summary>
    public static class MetaScalarConverter
    {
        /// <summary>
        /// Converts an expression poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MetaScalarDto ToDto(this IBdoMetaScalar poco)
        {
            MetaScalarDto dto = new();
            dto.UpdateFromPoco(poco);

            return dto;
        }

        public static MetaScalarDto UpdateFromPoco(
            this MetaScalarDto dto,
            IBdoMetaScalar poco)
        {
            if (dto == null) return null;

            if (poco == null) return dto;

            MapperConfiguration config;

            config = new MapperConfiguration(
                cfg => cfg.CreateMap<IBdoMetaScalar, MetaScalarDto>()
                    .ForMember(q => q.ClassReference, opt => opt.Ignore())
                    .ForMember(q => q.Item, opt => opt.Ignore())
                    .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.Reference.ToDto()))
                    .ForMember(q => q.Schema, opt => opt.MapFrom(q => q.Schema.ToDto()))
            );

            var mapper = new Mapper(config);
            mapper.Map(poco, dto);

            dto.ClassReference = poco.DataType.IsSpecified() ? poco?.DataType.ToDto() : null;
            dto.DefinitionUniqueName = poco?.DataType?.DefinitionUniqueName;

            var dataList = poco.GetDataList<object>()?.Select(q => q.ToString(dto.ValueType)).ToList();
            if (dataList?.Count > 1)
            {
                dto.Items = dataList;
            }
            else
            {
                dto.Item = dataList?.FirstOrDefault();
            }

            dto.ValueType = poco?.DataType.ValueType ?? DataValueTypes.Any;
            if (poco.Schema?.DataType.ValueType == poco.DataType?.ValueType)
            {
                dto.ValueType = DataValueTypes.Any;
            }

            return dto;
        }

        /// <summary>
        /// Converts a meta scalar DTO to a poco one.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static IBdoMetaScalar ToPoco(
            this MetaScalarDto dto)
        {
            if (dto == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<MetaScalarDto, BdoMetaScalar>()
                    .ForMember(q => q.Reference, opt => opt.MapFrom(q => q.Reference.ToPoco()))
                    .ForMember(q => q.DataType, opt => opt.Ignore())
                    .ForMember(q => q.Parent, opt => opt.Ignore())
                    .ForMember(q => q.Schema, opt => opt.Ignore())
                );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoMetaScalar>(dto);

            poco.DataType = new BdoDataType(dto?.ClassReference?.ToPoco())
            {
                DefinitionUniqueName = dto.DefinitionUniqueName,
                Identifier = dto.Identifier,
                ValueType = dto.ValueType
            };
            poco.Schema = dto.Schema.ToPoco();

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
