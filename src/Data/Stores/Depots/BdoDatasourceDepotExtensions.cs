using BindOpen.Logging;
using BindOpen.Scoping;
using System;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// This class represents an data source extensions.
    /// </summary>
    public static class BdoDatasourceDepotExtensions
    {
        /// <summary>
        /// Add a datasource depot into the specified data store executing the specified action.
        /// </summary>
        /// <param key="depotStore">The data store to consider.</param>
        /// <param key="action">The action to execute on the created depot.</param>
        /// <returns>Returns the data store to update.</returns>
        public static T RegisterDatasources<T>(
            this T depotStore,
            Action<IBdoDatasourceDepot> action = null)
            where T : IBdoDepotStore
            => RegisterDatasources<T>(depotStore, (d, l) => action?.Invoke(d));

        /// <summary>
        /// Add a data source depot into the specified data store using the specified options.
        /// </summary>
        /// <param key="depotStore">The data store to consider.</param>
        /// <param key="action">The action to execute on the created depot.</param>
        /// <returns>Returns the data store to update.</returns>
        public static T RegisterDatasources<T>(
            this T depotStore,
            Action<IBdoDatasourceDepot, IBdoLog> action)
            where T : IBdoDepotStore
        {
            var depot = new BdoDatasourceDepot()
            {
                LazyLoadFunction = (IBdoDepot d, IBdoLog log) =>
                {
                    var number = 0;

                    if (d is IBdoDatasourceDepot datasourceDepot)
                    {
                        action?.Invoke(datasourceDepot, log);

                        number = datasourceDepot.Count;

                        if (log?.HasEvent(BdoEventLevels.Error, BdoEventLevels.Fatal) == false)
                        {
                            log.AddEvent(BdoEventLevels.Information, "Depot loaded (" + number + " data sources added)");
                        }
                    }

                    return number;
                }
            };

            // we populate the data source depot from settings

            depotStore?.Add<IBdoDatasourceDepot>(depot);

            return depotStore;
        }

        /// <summary>
        /// Gets the datasource depot of the specified data store.
        /// </summary>
        /// <param key="depotStore">The data store to consider.</param>
        /// <returns>Returns the datasource depot of the specified data store.</returns>
        public static IBdoDatasourceDepot GetDatasourceDepot(this IBdoDepotStore depotStore)
        {
            return depotStore?.Get<IBdoDatasourceDepot>();
        }

        /// <summary>
        /// Gets the datasource depot of the specified scope.
        /// </summary>
        /// <param key="scope">The data store to consider.</param>
        /// <returns>Returns the datasource depot of the specified scope.</returns>
        public static IBdoDatasourceDepot GetDatasourceDepot(this IBdoScope scope)
        {
            return scope?.DepotStore?.Get<IBdoDatasourceDepot>();
        }

        /// <summary>
        /// Adds sources from connection strings.
        /// </summary>
        /// <param key="depot">The datasource depot to consider.</param>
        /// <param key="config">The config to consider.</param>
        /// <param key="keyName">The key name to consider.</param>
        public static IBdoDatasourceDepot Add<T>(
            this T depot,
            IBdoDatasource source)
            where T : IBdoDatasourceDepot
        {
            depot?.Add<IBdoDatasourceDepot, IBdoDatasource>(source);

            return depot;
        }
    }
}