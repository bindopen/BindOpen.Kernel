using BindOpen.Logging;

namespace BindOpen.Scoping
{
    /// <summary>
    /// This interface defines an extension store loader.
    /// </summary>
    public interface IBdoExtensionStoreLoader
    {
        /// <summary>
        /// Loads the packages.
        /// </summary>
        /// <param key="log">The log to populate.</param>
        /// <returns>Returns True whether this instance has loaded the packages.</returns>
        bool LoadPackages(IBdoLog log = null);
    }
}