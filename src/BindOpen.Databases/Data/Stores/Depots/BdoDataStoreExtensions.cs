using BindOpen.Application.Scopes;
using BindOpen.Data.Models;
using BindOpen.System.Diagnostics;
using System;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// This class represents an data queries factory.
    /// </summary>
    public static class BdoDataStoreExtensions
    {
        /// <summary>
        /// Add a database queries depot into the specified data store executing the specified action.
        /// </summary>
        /// <param name="dataStore">The data store to consider.</param>
        /// <returns>Returns the data store to update.</returns>
        public static IBdoDataStore RegisterDbModels(
            this IBdoDataStore dataStore) =>
            RegisterDbModels(dataStore, (d, l) => { });

        /// <summary>
        /// Add a database queries depot into the specified data store executing the specified action.
        /// </summary>
        /// <param name="dataStore">The data store to consider.</param>
        /// <param name="action">The action to execute on the created depot.</param>
        /// <returns>Returns the data store to update.</returns>
        public static IBdoDataStore RegisterDbModels(
            this IBdoDataStore dataStore,
            Action<IBdoDbModelDepot> action) =>
            RegisterDbModels(dataStore, (d, l) => action?.Invoke(d));

        /// <summary>
        /// Add a database queries depot into the specified data store executing the specified action.
        /// </summary>
        /// <param name="dataStore">The data store to consider.</param>
        /// <param name="action">The action to execute on the created depot.</param>
        /// <returns>Returns the data store to update.</returns>
        public static IBdoDataStore RegisterDbModels(
            this IBdoDataStore dataStore,
            Action<IBdoDbModelDepot, IBdoLog> action)
        {
            var depot = new BdoDbModelDepot()
            {
                LazyLoadFunction = (IBdoDepot d, IBdoLog log) =>
                {
                    var number = 0;

                    if (d is IBdoDbModelDepot dbModelDepot)
                    {
                        action?.Invoke(dbModelDepot, log);

                        number = dbModelDepot.Count;

                        if (!log.HasErrorsOrExceptions())
                        {
                            log.AddMessage("Depot loaded (" + number + " models added)");
                        }
                    }

                    return number;
                }
            };

            // we populate the data source depot from settings

            dataStore?.Add<IBdoDbModelDepot>(depot);
            return dataStore;
        }

        /// <summary>
        /// Gets the database queries depot of the specified data store.
        /// </summary>
        /// <param name="dataStore">The data store to consider.</param>
        /// <returns>Returns the database model depot of the specified data store.</returns>
        public static IBdoDbModelDepot GetDbModelDepot(this IBdoDataStore dataStore)
        {
            return dataStore?.Get<IBdoDbModelDepot>();
        }

        /// <summary>
        /// Gets the database model depot of the specified scope.
        /// </summary>
        /// <param name="scope">The data store to consider.</param>
        /// <returns>Returns the database model depot of the specified scope.</returns>
        public static IBdoDbModelDepot GetDbModelDepot(this IBdoScope scope)
        {
            return scope?.DataStore?.Get<IBdoDbModelDepot>();
        }

        /// <summary>
        /// Gets the database model with the specified name.
        /// </summary>
        /// <param name="scope">The data store to consider.</param>
        /// <returns>Returns the database query with the specified name.</returns>
        public static T GetModel<T>(this IBdoScope scope) where T : BdoDbModel
        {
            return scope?.DataStore?.Get<IBdoDbModelDepot>()?.GetModel<T>();
        }
    }
}