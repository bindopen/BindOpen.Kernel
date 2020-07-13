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
        /// Sets the console logger at startup.
        /// </summary>
        /// <param name="options">The BindOpen host options to consider.</param>
        /// <param name="initBuilder">The logger builder action to consider.</param>
        /// <returns></returns>
        public static ITBdoHostOptions<S> SetConsoleLoggerAtStartup<S>(
            this ITBdoHostOptions<S> options)
            where S : class, IBdoAppSettings, new()
        {
            options.SetLogger(h =>
            {
                var config = new LoggerConfiguration();
                config.WriteTo.Console();

                var loggerFactory = new LoggerFactory();
                loggerFactory.AddSerilog(config.CreateLogger());
                return loggerFactory.CreateLogger<IBdoHost>();
            });

            return options;
        }

        /// <summary>
        /// Sets the logger.
        /// </summary>
        /// <param name="options">The BindOpen host options to consider.</param>
        /// <param name="initBuilder">The logger builder action to consider.</param>
        /// <returns></returns>
        public static ITBdoHostOptions<S> SetLogger<S>(
            this ITBdoHostOptions<S> options,
            Func<LoggerConfiguration, LoggerConfiguration> initLoggerConfig)
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

            return options;
        }

        /// <summary>
        /// Add a BindOpen console logger.
        /// </summary>
        /// <param name="builder">The logging builder to consider.</param>
        /// <param name="options">The BindOpen host options to consider.</param>
        /// <returns></returns>
        public static LoggerConfiguration AddConsole(
            this LoggerConfiguration config)
        {
            config?.WriteTo.Console();
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
            config?.WriteTo.File(
                (options?.HostSettings?.LogsFolderPath + options?.HostSettings?.LogsFileName).ToPath(),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: options?.HostSettings?.LogsExpirationDayNumber);
            return config;
        }
    }
}
