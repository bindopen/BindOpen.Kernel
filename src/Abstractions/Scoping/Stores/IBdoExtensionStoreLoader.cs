using BindOpen.System.Diagnostics.Logging;

namespace BindOpen.System.Scoping.Stores
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
        IBdoExtensionStoreLoader LoadPackages(IBdoLog log = null);
    }
}