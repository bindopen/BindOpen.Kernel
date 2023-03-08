using BindOpen.Logging;
using BindOpen.Scoping.Scopes;
using System;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// This class represents an data source extensions.
    /// </summary>
    public static class BdoDataStoreExtensions
    {
        /// <summary>
        /// Add a datasource depot into the specified data store executing the specified action.
        /// </summary>
        /// <param key="dataStore">The data store to consider.</param>
        /// <returns>Returns the data store to update.</returns>
        public static IBdoDataStore RegisterDatasources(
            this IBdoDataStore dataStore) =>
            RegisterDatasources(dataStore, (d, l) => { });

        /// <summary>
        /// Add a datasource depot into the specified data store executing the specified action.
        /// </summary>
        /// <param key="dataStore">The data store to consider.</param>
        /// <param key="action">The action to execute on the created depot.</param>
        /// <returns>Returns the data store to update.</returns>
        public static IBdoDataStore RegisterDatasources(
            this IBdoDataStore dataStore,
            Action<IBdoSourceDepot> action) =>
            RegisterDatasources(dataStore, (d, l) => action?.Invoke(d));

        /// <summary>
        /// Add a data source depot into the specified data store using the specified options.
        /// </summary>
        /// <param key="dataStore">The data store to consider.</param>
        /// <param key="action">The action to execute on the created depot.</param>
        /// <returns>Returns the data store to update.</returns>
        public static IBdoDataStore RegisterDatasources(
            this IBdoDataStore dataStore,
            Action<IBdoSourceDepot, IBdoLog> action)
        {
            var depot = new BdoDatasourceDepot()
            {
                LazyLoadFunction = (IBdoDepot d, IBdoLog log) =>
                {
                    var number = 0;

                    if (d is IBdoSourceDepot datasourceDepot)
                    {
                        action?.Invoke(datasourceDepot, log);

                        number = datasourceDepot.Count;

                        if (log?.HasEvent(EventKinds.Error, EventKinds.Exception) == false)
                        {
                            log.AddMessage("Depot loaded (" + number + " data sources added)");
                        }
                    }

                    return number;
                }
            };

            // we populate the data source depot from settings

            dataStore?.Add<IBdoSourceDepot>(depot);
            return dataStore;
        }

        /// <summary>
        /// Gets the datasource depot of the specified data store.
        /// </summary>
        /// <param key="dataStore">The data store to consider.</param>
        /// <returns>Returns the datasource depot of the specified data store.</returns>
        public static IBdoSourceDepot GetDatasourceDepot(this IBdoDataStore dataStore)
        {
            return dataStore?.Get<IBdoSourceDepot>();
        }

        /// <summary>
        /// Gets the datasource depot of the specified scope.
        /// </summary>
        /// <param key="scope">The data store to consider.</param>
        /// <returns>Returns the datasource depot of the specified scope.</returns>
        public static IBdoSourceDepot GetDatasourceDepot(this IBdoScope scope)
        {
            return scope?.DataStore?.Get<IBdoSourceDepot>();
        }
    }
}