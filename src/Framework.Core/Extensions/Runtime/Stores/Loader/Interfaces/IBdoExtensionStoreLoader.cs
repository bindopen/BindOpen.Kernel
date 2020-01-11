using BindOpen.Framework.Data.Items;
using BindOpen.Framework.System.Assemblies.References;
using BindOpen.Framework.System.Diagnostics;

namespace BindOpen.Framework.Extensions.Runtime
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoExtensionStoreLoader : IDataItem
    {
        /// <summary>
        /// Loads the specified extensions into the specified scope.
        /// </summary>
        /// <param name="references">The library references to consider.</param>
        IBdoLog LoadExtensionsInStore(params IBdoAssemblyReference[] references);
    }
}