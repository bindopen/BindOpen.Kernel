namespace BindOpen.System.Data.Conditions
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class ReferenceConditionConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ReferenceConditionDto ToDto(this IBdoReferenceCondition poco)
        {
            if (poco == null) return null;

            ReferenceConditionDto dto = new()
            {
                Reference = poco.Reference.ToDto()
            };

            return dto;
        }

        /// <summary>
        /// Converts to POCO.
        /// </summary>
        /// <param key="dto">The dto to consider.</param>
        /// <returns>The POCO object.</returns>
        public static IBdoReferenceCondition ToPoco(
            this ReferenceConditionDto dto)
        {
            if (dto == null) return null;

            BdoReferenceCondition poco = new()
            {
                Reference = dto.Reference.ToPoco()
            };

            return poco;
        }
    }
}
