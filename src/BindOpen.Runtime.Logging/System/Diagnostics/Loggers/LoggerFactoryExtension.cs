using BindOpen.Application.Settings;
using BindOpen.Data.Helpers.Files;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace BindOpen.Application.Scopes
{
    /// <summary>
    /// This class extends the LoggerFactory class.
    /// </summary>
    public static class LoggerFactoryExtension
    {
        /// <summary>
        /// Sets the logger.
        /// </summary>
        /// <param name="options">The BindOpen host options to consider.</param>
        /// <param name="initBuilder">The logger builder action to consider.</param>
        /// <returns></returns>
        public static ITBdoHostOptions<S> SetLogger<S>(
            this ITBdoHostOptions<S> options,
            Func<LoggerConfiguration, LoggerConfiguration> initLoggerConfig,
            bool isTraceLoggerAtStartup = false)
            where S : class, IBdoAppSettings, new()
        {
            options.SetLogger(h =>
            {
                var config = new LoggerConfiguration();
                initLoggerConfig?.Invoke(config);

                var loggerFactory = new LoggerFactory();
                loggerFactory.AddSerilog(config.CreateLogger());
                return loggerFactory.CreateLogger<IBdoHost>();
            });

            if (isTraceLoggerAtStartup)
            {
                var config = new LoggerConfiguration();
                config.WriteTo.Trace();

                var loggerFactory = new LoggerFactory();
                loggerFactory.AddSerilog(config.CreateLogger());

                options.SetLoggerAtStartup(loggerFactory);
            }

            return options;
        }

        /// <summary>
        /// Add a BindOpen trace logger.
        /// </summary>
        /// <param name="builder">The logging builder to consider.</param>
        /// <param name="options">The BindOpen host options to consider.</param>
        /// <returns></returns>
        public static LoggerConfiguration AddTrace(
            this LoggerConfiguration config)
        {
            config?.WriteTo.Trace();
            return config;
        }

        /// <summary>
        /// Add a BindOpen file logger.
        /// </summary>
        /// <param name="builder">The logging builder to consider.</param>
        /// <param name="options">The BindOpen host options to consider.</param>
        /// <returns></returns>
        public static LoggerConfiguration AddFile<S>(
            this LoggerConfiguration config,
            ITBdoHostOptions<S> options)
            where S : class, IBdoAppSettings, new()
        {
            int? expirationDayNumber = options?.HostSettings?.LogsExpirationDayNumber < 0 ?
                null : options?.HostSettings?.LogsExpirationDayNumber;

            config?.WriteTo.File(
                (options?.HostSettings?.LogsFolderPath + options?.HostSettings?.LogsFileName).ToPath(),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: expirationDayNumber);

            return config;
        }
    }
}
