using System.Linq;

namespace BindOpen.Framework.Core.System.Diagnostics.Dto
{
    /// <summary>
    /// This class represents a factory for API logs.
    /// </summary>
    public static class ApiLogDtoFactory
    {
        /// <summary>
        /// Converts the specified log to the Api log DTO.
        /// </summary>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the Api log DTO.</returns>
        public static ApiLogDto ToApiDto(this ILog log, string key = "*", string alternateKey = null)
            => log == null ? null : new ApiLogDto()
            {
                CreationDate = log.CreationDate,
                Description = log.Description?.GetContent(key, alternateKey),
                DisplayName = log.Title?.GetContent(key, alternateKey),
                Events = log.Events.Select(q => q.ToApiDto()).ToList(),
                Id = log.Id,
                LastModificationDate = log.LastModificationDate,
                Name = log.Name
            };

        /// <summary>
        /// Converts the specified log to the Api log DTO.
        /// </summary>
        /// <param name="aEvent">The log to consider.</param>
        /// <returns>Returns the Api log DTO.</returns>
        public static ApiLogEventDto ToApiDto(this ILogEvent aEvent, string key = "*", string alternateKey = null)
        {
            return aEvent == null ? null : new ApiLogEventDto()
            {
                CreationDate = aEvent.CreationDate,
                Criticality = aEvent.Criticality,
                Date = aEvent.Date,
                Description = aEvent.Description?.GetContent(key, alternateKey),
                DisplayName = aEvent.Title?.GetContent(key, alternateKey),
                Id = aEvent.Id,
                Kind = aEvent.Kind,
                LastModificationDate = aEvent.LastModificationDate,
                Name = aEvent.Name,
                ResultCode = aEvent.ResultCode
            };
        }
    }
}