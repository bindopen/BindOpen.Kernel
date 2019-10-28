using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Runtime.Application.Modules;
using BindOpen.Framework.Runtime.Application.Settings;
using System.Collections.Generic;

namespace BindOpen.Framework.Runtime.Application.Options
{
    /// <summary>
    /// The interface defines the base BDO application host options.
    /// </summary>
    public interface IAppHostOptions
    {
        /// <summary>
        /// The base settings.
        /// </summary>
        IAppSettings Settings { get; }

        /// <summary>
        /// The application module.
        /// </summary>
        IAppModule ApplicationModule { get; }

        /// <summary>
        /// Indicates whether the default logger is used.
        /// </summary>
        bool IsDefaultLoggerUsed { get; }

        /// <summary>
        /// The loggers.
        /// </summary>
        IList<ILogger> Loggers { get; }

        /// <summary>
        /// The extension configuration.
        /// </summary>
        IAppExtensionConfiguration ExtensionConfiguration { get; }

        // Paths ----------------------

        /// <summary>
        /// The application folder path.
        /// </summary>
        string AppFolderPath { get; }

        /// <summary>
        /// The library folder path.
        /// </summary>
        string LibraryFolderPath { get; }

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
        IDataElementSpecSet SettingsSpecificationSet { get; set; }

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

        ///// <summary>
        ///// Define the specified settings.
        ///// </summary>
        ///// <param name="specificationSet">The set of data element specifcations to consider.</param>
        ///// <returns>Returns the application host option.</returns>
        //IAppHostOptions DefineUserSettings(IDataElementSpecSet specificationSet = null);

        /// <summary>
        /// Define the default specified settings.
        /// </summary>
        /// <param name="specificationSet">The set of data element specifcations to consider.</param>
        /// <returns>Returns the application host option.</returns>
        IAppHostOptions DefineDefaultSettings(IDataElementSpecSet specificationSet = null);

        /// <summary>
        /// Sets the extensions.
        /// </summary>
        /// <param name="extensionConfiguration">The extension configuration.</param>
        /// <returns>Returns the application host option.</returns>
        IAppHostOptions SetExtensions(IAppExtensionConfiguration extensionConfiguration);

        /// <summary>
        /// Adds the extensions.
        /// </summary>
        /// <param name="filters">The filters to consider.</param>
        /// <returns>Returns the application host option.</returns>
        IAppHostOptions SetExtensions(params IAppExtensionFilter[] filters);

        /// <summary>
        /// Adds the default logger.
        /// </summary>
        /// <returns>Returns the application host option.</returns>
        IAppHostOptions AddDefaultLogger();

        /// <summary>
        /// Adds the specified loggers.
        /// </summary>
        /// <param name="loggers">The loggers to consider.</param>
        /// <returns>Returns the application host option.</returns>
        IAppHostOptions AddLoggers(params ILogger[] loggers);

        /// <summary>
        /// Set the module.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <returns>Returns the application host option.</returns>
        IAppHostOptions SetModule(IAppModule module);

        /// <summary>
        /// Set the module.
        /// </summary>
        /// <param name="moduleName">The name of the module.</param>
        /// <returns>Returns the application host option.</returns>
        IAppHostOptions SetModule(string moduleName);
    }
}