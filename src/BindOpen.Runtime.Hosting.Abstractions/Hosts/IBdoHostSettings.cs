using BindOpen.Runtime.Settings;

namespace BindOpen.Runtime.Hosts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoHostSettings : IBdoSettings
    {
        // Paths ----------------------

        /// <summary>
        /// 
        /// </summary>
        string LibraryFolderPath { get; }

        /// <summary>
        /// Set the library folder.
        /// </summary>
        /// <param name="libraryFolderPath">The library folder path.</param>
        /// <returns>Returns the host option.</returns>
        IBdoHostSettings WithLibraryFolder(string libraryFolderPath);
    }
}