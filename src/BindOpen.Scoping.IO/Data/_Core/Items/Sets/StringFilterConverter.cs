using System.Collections.Generic;

namespace BindOpen.Scoping.Data
{
    /// <summary>
    /// This static class represents a data reference converter.
    /// </summary>
    public static class StringSetConverter
    {
        /// <summary>
        /// Converts to DTO.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static StringSetDto ToDto(this IBdoStringSet poco)
        {
            if (poco == null) return null;

            StringSetDto dto = new()
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
        public static IBdoStringSet ToPoco(this StringSetDto dto)
        {
            BdoStringSet poco = new()
            {
                AddedValues = new List<string>(dto?.AddedValues),
                RemovedValues = new List<string>(dto?.RemovedValues)
            };

            return poco;
        }
    }
}
