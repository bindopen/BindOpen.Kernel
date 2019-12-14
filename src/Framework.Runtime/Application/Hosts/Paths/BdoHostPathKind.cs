namespace BindOpen.Framework.Runtime.Application.Hosts
{
    /// <summary>
    /// This enumeration lists all the possible kinds of host path.
    /// </summary>
    public enum BdoHostPathKind
    {
        /// <summary>
        /// The root folder.
        /// </summary>
        RootFolder,

        /// <summary>
        /// The primary log folder.
        /// </summary>
        PrimaryLogsFolder,

        /// <summary>
        /// The extensions folder.
        /// </summary>
        PackagesFolder,

        /// <summary>
        /// The library folder.
        /// </summary>
        LibraryFolder,

        /// <summary>
        /// The log folder.
        /// </summary>
        LogsFolder,

        /// <summary>
        /// The roaming folder.
        /// </summary>
        RoamingFolder,

        /// <summary>
        /// The runtime folder.
        /// </summary>
        RuntimeFolder,

        /// <summary>
        /// The host configuration file.
        /// </summary>
        HostConfigFile,

        /// <summary>
        /// The application configuration file.
        /// </summary>
        AppConfigFile,

        /// <summary>
        /// The settings folder.
        /// </summary>
        ConfigurationFolder,

        /// <summary>
        /// The temporary folder.
        /// </summary>
        TemporaryFolder,

        /// <summary>
        /// The users folder.
        /// </summary>
        UsersFolder,
    }
}