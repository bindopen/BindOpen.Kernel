namespace BindOpen.Data.Configuration
{
    /// <summary>
    /// This class represents a Xml helper.
    /// </summary>
    public static class ConfigurationConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param name="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static ConfigurationDto ToDto(this IBdoConfiguration poco)
        {
            if (poco == null) return null;

            ConfigurationDto dto = new()
            {
                CreationDate = StringHelper.ToString(poco.CreationDate),
                //Description = poco.Description.ToDto(),
                //Items = poco.Items?.Select(q=>q.ToDto()).ToList(),
                LastModificationDate = StringHelper.ToString(poco.LastModificationDate),
                Name = poco.Name
            };

            return dto;
        }

        /// <summary>
        /// Converts to POCO.
        /// </summary>
        /// <param name="dto">The dto to consider.</param>
        /// <returns>The POCO object.</returns>
        public static IBdoConfiguration ToPoco(this ConfigurationDto dto)
        {
            if (dto == null) return null;

            BdoConfiguration poco = new()
            {
                CreationDate = dto.CreationDate.ToDateTime(),
                //Description = dto.Description.ToPoco(),
                //Items = dto.Items?.Select(q => q.ToPoco()).ToList(),
                LastModificationDate = dto.LastModificationDate.ToDateTime(),
                Name = dto.Name
            };

            return poco;
        }
    }
}
