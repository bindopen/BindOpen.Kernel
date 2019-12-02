using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Extensions.References;
using BindOpen.Framework.Core.Extensions.Runtime.Stores;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Runtime.Application.Modules;
using BindOpen.Framework.Runtime.Application.Settings.Hosts;
using System;
using System.Collections.Generic;

namespace BindOpen.Framework.Runtime.Application.Options.Hosts
{
    /// <summary>
    /// The interface defines the host options.
    /// </summary>
    public interface ITBdoHostOptions<S> : IBdoHostOptions where S : class, IBdoHostSettings, new()
    {
        /// <summary>
        /// Sets the specified environment.
        /// </summary>
        /// <param name="environment">The environment to consider.</param>
        /// <returns>Returns this instance.</returns>
        ITBdoHostOptions<S> SetEnvironment(string environment);

        // Paths ----------------------

        /// <summary>
        /// Set the application folder.
        /// </summary>
        /// <param name="appFolderPath">The application folder path.</param>
        /// <returns>Returns the host option.</returns>
        ITBdoHostOptions<S> SetAppFolder(string appFolderPath);

        /// <summary>
        /// Set the settings file.
        /// </summary>
        /// <param name="settingsFilePath">The settings file path.</param>
        /// <returns>Returns the host option.</returns>
        ITBdoHostOptions<S> SetSettingsFile(string settingsFilePath);

        // Settings ----------------------

        /// <summary>
        /// The settings.
        /// </summary>
        S Settings { get; set; }

        /// <summary>
        /// Defines the application settings.
        /// </summary>
        /// <param name="action">The action to consider on the settings.</param>
        /// <returns>Returns the host option.</returns>
        ITBdoHostOptions<S> SetAppSettings(Action<IBdoHostAppSettings> action);

        /// <summary>
        /// Defines the specified settings.
        /// </summary>
        /// <param name="specificationSet">The set of data element specifcations to consider.</param>
        /// <returns>Returns the host option.</returns>
        ITBdoHostOptions<S> DefineSettings(IDataElementSpecSet specificationSet = null);

        // Extensions ----------------------

        /// <summary>
        /// Configures the extension load of this instance.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        ITBdoHostOptions<S> ConfigureExtensionLoad(Action<IExtensionLoadOptions> action);

        /// <summary>
        /// Adds the extensions.
        /// </summary>
        /// <returns>Returns the host option.</returns>
        ITBdoHostOptions<S> AddExtensions(Action<List<IBdoExtensionReference>> action);

        // Loggers ----------------------

        /// <summary>
        /// Adds the default file logger.
        /// </summary>
        /// <param name="logFileName">The log file name to consider.</param>
        /// <returns>Returns the host option.</returns>
        ITBdoHostOptions<S> AddDefaultFileLogger(string logFileName = null);

        /// <summary>
        /// Adds the default file logger.
        /// </summary>
        /// <returns>Returns the host option.</returns>
        ITBdoHostOptions<S> AddDefaultConsoleLogger();

        /// <summary>
        /// Adds the specified loggers.
        /// </summary>
        /// <param name="loggers">The loggers to consider.</param>
        /// <returns>Returns the host option.</returns>
        ITBdoHostOptions<S> AddLoggers(params IBdoLogger[] loggers);

        // Modules -------------------------------------------

        /// <summary>
        /// Set the module.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <returns>Returns the host option.</returns>
        ITBdoHostOptions<S> SetModule(IAppModule module);

        /// <summary>
        /// Set the module.
        /// </summary>
        /// <param name="moduleName">The name of the module.</param>
        /// <returns>Returns the host option.</returns>
        ITBdoHostOptions<S> SetModule(string moduleName);
    }
}