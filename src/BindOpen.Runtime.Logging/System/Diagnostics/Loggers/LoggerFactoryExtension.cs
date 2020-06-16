using BindOpen.Application.Settings;
using Microsoft.Extensions.Logging;

namespace BindOpen.Application.Scopes
{
    /// <summary>
    /// This class extends the LoggerFactory class.
    /// </summary>
    public static class LoggerFactoryExtension
    {
        /// <summary>
        /// Add a BindOpen file logger.
        /// </summary>
        /// <param name="builder">The logging builder to consider.</param>
        /// <param name="options">The BindOpen host options to consider.</param>
        /// <returns></returns>
        public static ILoggingBuilder AddBindOpenFileLogger<S>(
            this ILoggingBuilder builder, ITBdoHostOptions<S> options)
             where S : class, IBdoAppSettings, new()
        {
            return builder;
        }
    }
}
