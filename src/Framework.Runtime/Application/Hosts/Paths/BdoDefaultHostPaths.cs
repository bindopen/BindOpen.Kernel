using BindOpen.Framework.Core.Data.Helpers.Strings;
using System.IO;
using System.Reflection;

namespace BindOpen.Framework.Runtime.Application.Settings.Hosts
{
    /// <summary>
    /// This static class contains the default option values.
    /// </summary>
    public static class BdoDefaultHostPaths
    {
        // ------------------------------------------
        // CONSTANTS
        // ------------------------------------------

        #region Constants

        /// <summary>
        /// The default root folder
        /// </summary>
        public static readonly string __DefaultRootFolderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).GetEndedString(@"\").ToPath();

        /// <summary>
        /// The default host settings file name
        /// </summary>
        public static readonly string __DefaultAppSettingsFileName = "bindopen.xml";

        /// <summary>
        /// The default application configuration file name
        /// </summary>
        public static readonly string __DefaultConfigurationFileName = "bindopen.config.xml";

        /// <summary>
        /// The default runtime folder path.
        /// </summary>
        public static readonly string __DefaultRuntimeFolderPath = @"bdo\".ToPath();

        /// <summary>
        /// The default settings folder path.
        /// </summary>
        public static readonly string __DefaultConfigurationFolderPath = @"config\".ToPath();

        /// <summary>
        /// The default logs folder path.
        /// </summary>
        public static readonly string __DefaultLogsFolderPath = @"logs\".ToPath();

        /// <summary>
        /// The default library folder path.
        /// </summary>
        public static readonly string __DefaultLibraryFolderPath = @"lib\".ToPath();

        /// <summary>
        /// The default extensions folder path.
        /// </summary>
        public static readonly string __DefaultPackagesFolderPath = @"packages\".ToPath();

        /// <summary>
        /// The default log file name.
        /// </summary>
        public static readonly string __DefaultLogsFileName = __DefaultPrimaryLogsFileNamePreffix + @"$(id).txt";

        /// <summary>
        /// The preffix of the default primary logs file name.
        /// </summary>
        public static readonly string __DefaultPrimaryLogsFileNamePreffix = @"log_$(timestamp)_";

        /// <summary>
        /// Maximum number of minutes : authentication 
        /// </summary>
        public static readonly int _AUTHENTICATIONTIMEOUT = 30;

        #endregion
    }
}