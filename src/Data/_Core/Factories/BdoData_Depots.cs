using BindOpen.Kernel.Data.Stores;
using System;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This static class provides methods to handle configs.
    /// </summary>
    public static partial class BdoData
    {
        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        public static BdoDepotStore NewDepotStore(
            Action<IBdoDepotStore> initializer = null)
        {
            var store = new BdoDepotStore();
            initializer?.Invoke(store);
            return store;
        }
    }
}
