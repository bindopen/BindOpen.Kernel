using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Stores;
using BindOpen.Framework.Core.Extensions.References;
using BindOpen.Framework.Core.Extensions.Runtime.Stores;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;
using BindOpen.Framework.Runtime.Application.Modules;
using BindOpen.Framework.Runtime.Application.Settings;
using System.Collections.Generic;

namespace BindOpen.Framework.Runtime.Application.Options
{
    /// <summary>
    /// The interface defines the base BDO host options.
    /// </summary>
    public interface IBdoHostOptions : IDataItem
    {
        // Modules ----------------------

        /// <summary>
        /// The application module.
        /// </summary>
        IAppModule ApplicationModule { get; }

        // Paths ----------------------

        /// <summary>
        /// The root folder path.
        /// </summary>
        string RootFolderPath { get; }

        /// <summary>
        /// The settings file path.
        /// </summary>
        string HostConfigFilePath { get; }

        /// <summary>
        /// Indicates whether the host settings file must exist.
        /// </summary>
        /// <remarks>If it does not exist then an exception is thrown.</remarks>
        bool? IsHostConfigFileRequired { get; }

        // Settings ----------------------

        /// <summary>
        /// The host settings.
        /// </summary>
        IBdoHostSettings HostSettings { get; }

        /// <summary>
        /// The application settings.
        /// </summary>
        IBdoAppSettings AppSettings { get; }

        /// <summary>
        /// The set of settings specifications of this instance.
        /// </summary>
        IDataElementSpecSet AppSettingsSpecificationSet { get; }

        // Extensions ----------------------

        /// <summary>
        /// The extension to load.
        /// </summary>
        List<IBdoExtensionReference> ExtensionReferences { get; }

        /// <summary>
        /// The extension load options.
        /// </summary>
        IExtensionLoadOptions ExtensionLoadOptions { get; }

        // Loggers ----------------------

        /// <summary>
        /// The output kinds of the default logger.
        /// </summary>
        /// <remarks>If there is none then we do not have any default logger.</remarks>
        HashSet<DatasourceKind> DefaultLoggerOutputKinds { get; }

        /// <summary>
        /// The loggers.
        /// </summary>
        IList<IBdoLogger> Loggers { get; }

        /// <summary>
        /// Get the settings as the specified host settings class.
        /// </summary>
        /// <typeparam name="T">The host settings class to consider.</typeparam>
        T GetSettings<T>() where T : class, IBdoAppSettings;

        // Depots ----------------------

        /// <summary>
        /// The depot sets of this instance.
        /// </summary>
        IBdoDataStore DataStore { get; }
    }
}