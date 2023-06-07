namespace BindOpen.Scoping.Data.Meta
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class SpecConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static SpecDto ToDto(this IBdoSpec poco)
        {
            if (poco == null) return null;

            SpecDto dto = new SpecDto();


            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoSpec ToPoco(
            this SpecDto dto)
        {
            if (dto == null) return null;

            BdoSpec poco = null;


            return poco;
        }
    }
}
