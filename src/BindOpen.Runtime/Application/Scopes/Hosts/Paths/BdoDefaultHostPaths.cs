using BindOpen.Data.Helpers.Files;

namespace BindOpen.Application.Scopes
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
        /// The default host settings file name
        /// </summary>
        public static readonly string __DefaultHostConfigFileName = "bindopen.xml";

        /// <summary>
        /// The default application configuration file name
        /// </summary>
        public static readonly string __DefaultAppConfigFileName = "appconfig.xml";

        /// <summary>
        /// The default runtime folder path.
        /// </summary>
        public static readonly string __DefaultRuntimeFolderPath = @"bdo\".ToPath();

        /// <summary>
        /// The default settings folder path.
        /// </summary>
        public static readonly string __DefaultAppConfigFolderPath = @"config\".ToPath();

        /// <summary>
        /// The default logs folder path.
        /// </summary>
        public static readonly string __DefaultLogsFolderPath = @"logs\".ToPath();

        /// <summary>
        /// The default library folder path.
        /// </summary>
        public static readonly string __DefaultLibraryFolderPath = @"lib\".ToPath();

        /// <summary>
        /// The default packages folder path.
        /// </summary>
        public static readonly string __DefaultPackagesFolderPath = @"packages\".ToPath();

        /// <summary>
        /// The default projects folder path.
        /// </summary>
        public static readonly string __DefaultProjectsFolderPath = @"projects\".ToPath();

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