﻿using AutoMapper;

namespace BindOpen.System.Data.Assemblies
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class ClassReferenceConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ClassReferenceDto ToDto(this IBdoClassReference poco)
        {
            if (poco == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<BdoClassReference, ClassReferenceDto>()
            );

            var mapper = new Mapper(config);
            var dto = mapper.Map<ClassReferenceDto>(poco);

            return dto;
        }

        public static bool IsSpecified(this IBdoClassReference poco) =>
            poco != null
            && (poco?.AssemblyFileName != null
            || poco?.AssemblyName != null
            || poco?.AssemblyVersion != null
            || poco?.ClassName != null);

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoClassReference ToPoco(
            this ClassReferenceDto dto)
        {
            if (dto == null) return null;

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<ClassReferenceDto, BdoClassReference>()
            );

            var mapper = new Mapper(config);
            var poco = mapper.Map<BdoClassReference>(dto);

            return poco;
        }
    }
}
