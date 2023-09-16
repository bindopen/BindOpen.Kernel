﻿using BindOpen.Kernel.Logging;
using BindOpen.Kernel.Scoping;
using System;

namespace BindOpen.Kernel.Data.Stores
{
    /// <summary>
    /// This class represents an data source extensions.
    /// </summary>
    public static class BdoDatasourceDepotExtensions
    {
        /// <summary>
        /// Add a datasource depot into the specified data store executing the specified action.
        /// </summary>
        /// <param key="dataStore">The data store to consider.</param>
        /// <param key="action">The action to execute on the created depot.</param>
        /// <returns>Returns the data store to update.</returns>
        public static T RegisterDatasources<T>(
            this T dataStore,
            Action<IBdoDatasourceDepot> action = null)
            where T : IBdoDepotStore
            => RegisterDatasources<T>(dataStore, (d, l) => action?.Invoke(d));

        /// <summary>
        /// Add a data source depot into the specified data store using the specified options.
        /// </summary>
        /// <param key="dataStore">The data store to consider.</param>
        /// <param key="action">The action to execute on the created depot.</param>
        /// <returns>Returns the data store to update.</returns>
        public static T RegisterDatasources<T>(
            this T dataStore,
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

                        if (log?.HasEvent(EventKinds.Error, EventKinds.Exception) == false)
                        {
                            log.AddEvent(EventKinds.Message, "Depot loaded (" + number + " data sources added)");
                        }
                    }

                    return number;
                }
            };

            // we populate the data source depot from settings

            dataStore?.Add<IBdoDatasourceDepot>(depot);

            return dataStore;
        }

        /// <summary>
        /// Gets the datasource depot of the specified data store.
        /// </summary>
        /// <param key="dataStore">The data store to consider.</param>
        /// <returns>Returns the datasource depot of the specified data store.</returns>
        public static IBdoDatasourceDepot GetDatasourceDepot(this IBdoDepotStore dataStore)
        {
            return dataStore?.Get<IBdoDatasourceDepot>();
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