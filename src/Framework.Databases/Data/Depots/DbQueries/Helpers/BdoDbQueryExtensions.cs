using BindOpen.Framework.Application.Scopes;
using BindOpen.Framework.Data.Stores;
using BindOpen.Framework.System.Diagnostics;
using System;

namespace BindOpen.Framework.Data.Depots
{
    /// <summary>
    /// This class represents an data queries factory.
    /// </summary>
    public static class BdoDbQueryExtensions
    {
        /// <summary>
        /// Add a database queries depot into the specified data store executing the specified action.
        /// </summary>
        /// <param name="dataStore">The data store to consider.</param>
        /// <param name="action">The action to execute on the created depot.</param>
        /// <returns>Returns the data store to update.</returns>
        public static IBdoDataStore RegisterDbQueryDepot(
            this IBdoDataStore dataStore,
            Action<IBdoDbQueryDepot> action) =>
            RegisterDbQueryDepot(dataStore, (d, l) => action?.Invoke(d));

        /// <summary>
        /// Add a database queries depot into the specified data store executing the specified action.
        /// </summary>
        /// <param name="dataStore">The data store to consider.</param>
        /// <param name="action">The action to execute on the created depot.</param>
        /// <returns>Returns the data store to update.</returns>
        public static IBdoDataStore RegisterDbQueryDepot(
            this IBdoDataStore dataStore,
            Action<IBdoDbQueryDepot, IBdoLog> action)
        {
            var depot = new BdoDbQueryDepot()
            {
                LazyLoadFunction = (IBdoDepot d, IBdoLog log) =>
                {
                    var number = 0;

                    if (d is IBdoDbQueryDepot dbQueryDepot)
                    {
                        action?.Invoke(dbQueryDepot, log);

                        number = dbQueryDepot.Count;

                        if (!log.HasErrorsOrExceptions())
                        {
                            log.AddMessage("Depot loaded (" + number + " queries added)");
                        }
                    }

                    return number;
                }
            };

            // we populate the data source depot from settings

            dataStore?.Add<IBdoDbQueryDepot>(depot);
            return dataStore;
        }

        /// <summary>
        /// Gets the database queries depot of the specified data store.
        /// </summary>
        /// <param name="dataStore">The data store to consider.</param>
        /// <returns>Returns the datasource depot of the specified data store.</returns>
        public static IBdoDbQueryDepot GetDbQueryDepot(this IBdoDataStore dataStore)
        {
            return dataStore?.Get<IBdoDbQueryDepot>();
        }

        /// <summary>
        /// Gets the datasource depot of the specified scope.
        /// </summary>
        /// <param name="scope">The data store to consider.</param>
        /// <returns>Returns the datasource depot of the specified scope.</returns>
        public static IBdoDbQueryDepot GetDbQueryDepot(this IBdoScope scope)
        {
            return scope?.DataStore?.Get<IBdoDbQueryDepot>();
        }
    }
}