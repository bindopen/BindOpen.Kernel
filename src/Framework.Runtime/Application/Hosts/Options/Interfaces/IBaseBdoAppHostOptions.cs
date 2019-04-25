using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Runtime.Application.Modules;
using BindOpen.Framework.Runtime.Application.Settings;

namespace BindOpen.Framework.Runtime.Application.Hosts.Options
{
    /// <summary>
    /// The interface defines the base BDO application host options.
    /// </summary>
    public interface IBaseBdoAppHostOptions
    {
        /// <summary>
        /// The base settings.
        /// </summary>
        IBaseBdoAppSettings BaseSettings { get; }

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
        ILogger[] Loggers { get; }

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
    }
}