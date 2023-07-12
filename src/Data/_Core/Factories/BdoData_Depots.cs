using BindOpen.System.Data.Stores;
using System;

namespace BindOpen.System.Data
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

        /// <summary>
        /// Instantiates a new instance of the Datasource class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="kind">The kind of the data source to consider.</param>
        public static IBdoSourceDepot NewDatasourceDepot(
            params IBdoDatasource[] datasources)
            => BdoData.NewSet<BdoDatasourceDepot, IBdoDatasource>(datasources);
    }
}
