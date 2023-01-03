using BindOpen.Runtime.References;
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
        /// <param name="references">The library references to consider.</param>
        /// <param name="log">The log to populate.</param>
        bool LoadExtensionsInStore(IBdoAssemblyReference[] references, IBdoLog log = null);
    }
}