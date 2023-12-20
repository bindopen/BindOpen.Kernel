using System.Collections.Generic;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a IO converter of mergers.
    /// </summary>
    public static class MergerConverter
    {
        /// <summary>
        /// Converts a merger poco into a DTO one.
        /// </summary>
        /// <param key="poco">The poco to consider.</param>
        /// <returns>The DTO object.</returns>
        public static MergerDto ToDto(this IBdoMerger poco)
        {
            if (poco == null) return null;

            MergerDto dto = new()
            {
                AddedValues = new List<string>(poco?.AddedValues),
                RemovedValues = new List<string>(poco?.RemovedValues)
            };

            return dto;
        }

        /// <summary>
        /// Converts a merger DTO to a poco one.
        /// </summary>
        /// <param key="dto">The DTO to consider.</param>
        /// <returns>The poco object.</returns>
        public static IBdoMerger ToPoco(this MergerDto dto)
        {
            BdoMerger poco = new()
            {
                AddedValues = new List<string>(dto?.AddedValues),
                RemovedValues = new List<string>(dto?.RemovedValues)
            };

            return poco;
        }
    }
}
