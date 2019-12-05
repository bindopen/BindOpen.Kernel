using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Assemblies.References;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Runtime.Stores
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