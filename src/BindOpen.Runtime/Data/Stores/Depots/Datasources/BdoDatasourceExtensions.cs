using BindOpen.Application.Scopes;
using BindOpen.System.Diagnostics;
using System;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// This class represents an data source extensions.
    /// </summary>
    public static class BdoDatasourceExtensions
    {
        /// <summary>
        /// Add a datasource depot into the specified data store executing the specified action.
        /// </summary>
        /// <param name="dataStore">The data store to consider.</param>
        /// <returns>Returns the data store to update.</returns>
        public static IBdoDataStore RegisterDatasources(
            this IBdoDataStore dataStore) =>
            RegisterDatasources(dataStore, (d, l) => { });

        /// <summary>
        /// Add a datasource depot into the specified data store executing the specified action.
        /// </summary>
        /// <param name="dataStore">The data store to consider.</param>
        /// <param name="action">The action to execute on the created depot.</param>
        /// <returns>Returns the data store to update.</returns>
        public static IBdoDataStore RegisterDatasources(
            this IBdoDataStore dataStore,
            Action<IBdoDatasourceDepot> action) =>
            RegisterDatasources(dataStore, (d, l) => action?.Invoke(d));

        /// <summary>
        /// Add a data source depot into the specified data store using the specified options.
        /// </summary>
        /// <param name="dataStore">The data store to consider.</param>
        /// <param name="action">The action to execute on the created depot.</param>
        /// <returns>Returns the data store to update.</returns>
        public static IBdoDataStore RegisterDatasources(
            this IBdoDataStore dataStore,
            Action<IBdoDatasourceDepot, IBdoLog> action)
        {
            var depot = new BdoDatasourceDepot()
            {
                LazyLoadFunction = (IBdoDepot d, IBdoLog log) =>
                {
                    var number = 0;

                    if (d is BdoDatasourceDepot datasourceDepot)
                    {
                        action?.Invoke(datasourceDepot, log);

                        number = datasourceDepot.Count;

                        if (!log.HasErrorsOrExceptions())
                        {
                            log.AddMessage("Depot loaded (" + number + " data sources added)");
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
        /// Adds sources from BindOpen configuration.
        /// </summary>
        /// <param name="options">The host options to consider.</param>
        public static IBdoDatasourceDepot AddFromConfiguration(this IBdoDatasourceDepot depot, IBdoHostOptions options)
        {
            if (options?.AppSettings?.AppConfiguration?.Datasources != null)
            {
                foreach (var dataSource in options?.AppSettings?.AppConfiguration?.Datasources)
                {
                    depot.Add(dataSource);
                }
            }

            return depot;
        }

        /// <summary>
        /// Gets the datasource depot of the specified data store.
        /// </summary>
        /// <param name="dataStore">The data store to consider.</param>
        /// <returns>Returns the datasource depot of the specified data store.</returns>
        public static IBdoDatasourceDepot GetDatasourceDepot(this IBdoDataStore dataStore)
        {
            return dataStore?.Get<IBdoDatasourceDepot>();
        }

        /// <summary>
        /// Gets the datasource depot of the specified scope.
        /// </summary>
        /// <param name="scope">The data store to consider.</param>
        /// <returns>Returns the datasource depot of the specified scope.</returns>
        public static IBdoDatasourceDepot GetDatasourceDepot(this IBdoScope scope)
        {
            return scope?.DataStore?.Get<IBdoDatasourceDepot>();
        }
    }
}