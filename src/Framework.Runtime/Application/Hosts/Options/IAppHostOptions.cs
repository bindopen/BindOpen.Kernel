using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Runtime.Application.Modules;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Runtime.Application.Hosts.Options
{
    /// <summary>
    /// The interface defines the application host configuration.
    /// </summary>
    public interface IAppHostOptions
    {
        /// <summary>
        /// The settings.
        /// </summary>
        AppSettings Settings { get; set; }

        /// <summary>
        /// The application module.
        /// </summary>
        AppModule ApplicationModule { get; }

        /// <summary>
        /// Indicates whether the default logger is used.
        /// </summary>
        bool IsDefaultLoggerUsed { get; }

        /// <summary>
        /// The loggers.
        /// </summary>
        Logger[] Loggers { get; }

        /// <summary>
        /// The extension configuration.
        /// </summary>
        AppExtensionConfiguration ExtensionConfiguration { get; }

        // Paths ----------------------

        /// <summary>
        /// The application folder path.
        /// </summary>
        string AppFolderPath { get; }

        /// <summary>
        /// The application configuration file path.
        /// </summary>
        string SettingsFilePath { get; }

        /// <summary>
        /// The runtime application folder path.
        /// </summary>
        string RuntimeFolderPath { get; }

        // Settings --------------------------------------

        /// <summary>
        /// The set of settings specifications of this instance.
        /// </summary>
        DataElementSpecSet SettingsSpecificationSet { get; set; }

        // Set -------------------------------------------

        /// <summary>
        /// Set the application folder.
        /// </summary>
        /// <param name="appFolderPath">The application folder path.</param>
        /// <returns>Returns the application host option.</returns>
        IAppHostOptions SetAppFolder(string appFolderPath);

        /// <summary>
        /// Set the runtime folder.
        /// </summary>
        /// <param name="runtimeFolderPath">The runtime folder path.</param>
        /// <returns>Returns the application host option.</returns>
        IAppHostOptions SetRuntimeFolder(string runtimeFolderPath);

        /// <summary>
        /// Set the library folder.
        /// </summary>
        /// <param name="libraryFolderPath">The library folder path.</param>
        /// <returns>Returns the application host option.</returns>
        IAppHostOptions SetLibraryFolder(string libraryFolderPath);

        /// <summary>
        /// Set the settings file.
        /// </summary>
        /// <typeparam name="T">The application settings class to consider.</typeparam>
        /// <param name="settingsFilePath">The path of the settings file.</param>
        /// <returns>Returns the application host option.</returns>
        IAppHostOptions SetSettingsFile<T>(string settingsFilePath = null) where T : AppSettings, new();

        /// <summary>
        /// Define the specified settings.
        /// </summary>
        /// <param name="specificationSet">The set of data element specifcations to consider.</param>
        /// <returns>Returns the application host option.</returns>
        IAppHostOptions DefineSettings(DataElementSpecSet specificationSet);

        /// <summary>
        /// Define the specified settings.
        /// </summary>
        /// <typeparam name="T">The application settings class to consider.</typeparam>
        /// <param name="specificationSet">The set of data element specifcations to consider.</param>
        /// <returns>Returns the application host option.</returns>
        IAppHostOptions DefineSettings<T>(DataElementSpecSet specificationSet = null) where T : AppSettings, new();

        /// <summary>
        /// Set the extensions.
        /// </summary>
        /// <param name="extensionConfiguration">The extension configuration.</param>
        /// <returns>Returns the application host option.</returns>
        IAppHostOptions SetExtensions(AppExtensionConfiguration extensionConfiguration);

        /// <summary>
        /// Adds the default logger.
        /// </summary>
        /// <returns>Returns the application host option.</returns>
        IAppHostOptions AddDefaultLogger();

        /// <summary>
        /// Set the specified loggers.
        /// </summary>
        /// <param name="loggers">The loggers to consider.</param>
        /// <returns>Returns the application host option.</returns>
        IAppHostOptions SetLoggers(params Logger[] loggers);

        /// <summary>
        /// Set the module.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <returns>Returns the application host option.</returns>
        IAppHostOptions SetModule(AppModule module);
    }
}