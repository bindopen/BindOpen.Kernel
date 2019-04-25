using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Runtime.Application.Configuration;
using BindOpen.Framework.Runtime.Application.Modules;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Runtime.Application.Options
{
    /// <summary>
    /// The interface defines the application host options.
    /// </summary>
    public interface ITBdoAppHostOptions<Q> : IBaseBdoAppHostOptions, IDataItem
        where Q : BdoAppConfiguration, new()
    {
        /// <summary>
        /// The settings.
        /// </summary>
        ITBdoAppSettings<Q> Settings { get; set; }

        // Set -------------------------------------------

        /// <summary>
        /// Set the application folder.
        /// </summary>
        /// <param name="appFolderPath">The application folder path.</param>
        /// <returns>Returns the application host option.</returns>
        ITBdoAppHostOptions<Q> SetAppFolder(string appFolderPath);

        /// <summary>
        /// Set the runtime folder.
        /// </summary>
        /// <param name="runtimeFolderPath">The runtime folder path.</param>
        /// <returns>Returns the application host option.</returns>
        ITBdoAppHostOptions<Q> SetRuntimeFolder(string runtimeFolderPath);

        /// <summary>
        /// Set the library folder.
        /// </summary>
        /// <param name="libraryFolderPath">The library folder path.</param>
        /// <returns>Returns the application host option.</returns>
        ITBdoAppHostOptions<Q> SetLibraryFolder(string libraryFolderPath);

        /// <summary>
        /// Set the settings file.
        /// </summary>
        /// <typeparam name="T">The application settings class to consider.</typeparam>
        /// <param name="settingsFilePath">The path of the settings file.</param>
        /// <returns>Returns the application host option.</returns>
        ITBdoAppHostOptions<Q> SetSettingsFile<T>(string settingsFilePath = null) where T : ITBdoAppSettings<Q>, new();

        /// <summary>
        /// Define the specified settings.
        /// </summary>
        /// <param name="specificationSet">The set of data element specifcations to consider.</param>
        /// <returns>Returns the application host option.</returns>
        ITBdoAppHostOptions<Q> DefineSettings(IDataElementSpecSet specificationSet);

        /// <summary>
        /// Define the specified settings.
        /// </summary>
        /// <typeparam name="T">The application settings class to consider.</typeparam>
        /// <param name="specificationSet">The set of data element specifcations to consider.</param>
        /// <returns>Returns the application host option.</returns>
        ITBdoAppHostOptions<Q> DefineSettings<T>(IDataElementSpecSet specificationSet = null)
            where T : ITBdoAppSettings<Q>, new();

        /// <summary>
        /// Set the extensions.
        /// </summary>
        /// <param name="extensionConfiguration">The extension configuration.</param>
        /// <returns>Returns the application host option.</returns>
        ITBdoAppHostOptions<Q> SetExtensions(IAppExtensionConfiguration extensionConfiguration);

        /// <summary>
        /// Adds the default logger.
        /// </summary>
        /// <returns>Returns the application host option.</returns>
        ITBdoAppHostOptions<Q> AddDefaultLogger();

        /// <summary>
        /// Set the specified loggers.
        /// </summary>
        /// <param name="loggers">The loggers to consider.</param>
        /// <returns>Returns the application host option.</returns>
        ITBdoAppHostOptions<Q> SetLoggers(params ILogger[] loggers);

        /// <summary>
        /// Set the module.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <returns>Returns the application host option.</returns>
        ITBdoAppHostOptions<Q> SetModule(IAppModule module);
    }
}