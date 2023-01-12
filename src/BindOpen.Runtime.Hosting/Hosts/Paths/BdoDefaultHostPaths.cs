using BindOpen.Meta;

namespace BindOpen.Runtime.Hosts
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
        /// The default library folder path.
        /// </summary>
        public static readonly string __DefaultLibraryFolderPath = @"lib\".ToPath();

        #endregion
    }
}