using BindOpen.Logging;

namespace BindOpen.Runtime.Stores
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoExtensionStoreLoader
    {
        /// <summary>
        /// Loads the specified extensions into the specified scope.
        /// </summary>
        /// <param key="references">The library references to consider.</param>
        /// <param key="log">The log to populate.</param>
        bool LoadPackages(IBdoLog log = null);
    }
}