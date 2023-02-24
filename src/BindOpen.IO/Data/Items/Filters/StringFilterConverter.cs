using System.Collections.Generic;

namespace BindOpen.Data.Items
{
    /// <summary>
    /// This static class represents a data reference converter.
    /// </summary>
    public static class StringFilterConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static StringFilterDto ToDto(this IBdoStringFilter poco)
        {
            if (poco == null) return null;

            StringFilterDto dto = new()
            {
                AddedValues = new List<string>(poco?.AddedValues),
                RemovedValues = new List<string>(poco?.RemovedValues)
            };

            return dto;
        }

        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The DTO object.</returns>
        public static IBdoStringFilter ToPoco(this StringFilterDto dto)
        {
            BdoStringFilter poco = new()
            {
                AddedValues = new List<string>(dto?.AddedValues),
                RemovedValues = new List<string>(dto?.RemovedValues)
            };

            return poco;
        }
    }
}
