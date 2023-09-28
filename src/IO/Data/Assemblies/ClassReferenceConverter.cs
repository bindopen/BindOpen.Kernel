using AutoMapper;

namespace BindOpen.Kernel.Data.Assemblies
{
    /// <summary>
    /// This class represents a IO converter of class references.
    /// </summary>
    public static class ClassReferenceConverter
    {
        /// <summary>
        /// Converts a class reference poco into a DTO one.
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

        /// <summary>
        /// Indicates whether the specified poco can be specified as DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>True if the poco can be specified as DTO.</returns>
        public static bool IsSpecified(this IBdoClassReference poco) =>
            poco != null
            && (poco?.AssemblyFileName != null
            || poco?.AssemblyName != null
            || poco?.AssemblyVersion != null
            || poco?.ClassName != null);

        /// <summary>
        /// Converts a class reference DTO into a poco one.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
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
