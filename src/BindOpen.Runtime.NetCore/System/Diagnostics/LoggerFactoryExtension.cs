using BindOpen.Application.Settings;
using Microsoft.Extensions.Logging;
using System;

namespace BindOpen.Application.Scopes
{
    /// <summary>
    /// This class extends the LoggerFactory class.
    /// </summary>
    public static class LoggerFactoryExtension
    {
        /// <summary>
        /// Adds the specified logger.
        /// </summary>
        /// <param name="options">The options to consider.</param>
        /// <param name="initAction">The initialization action to consider.</param>
        /// <returns>Returns the host option.</returns>
        public static ITBdoHostOptions<S> SetLogger<S>(this ITBdoHostOptions<S> options, Action<ILoggingBuilder> initAction)
             where S : class, IBdoAppSettings, new()
        {
            if (options != null)
            {
                var loggerFactory = LoggerFactory.Create(initAction);
                options.SetLogger(loggerFactory.CreateLogger<IBdoHost>());
            }

            return options;
        }
    }
}
