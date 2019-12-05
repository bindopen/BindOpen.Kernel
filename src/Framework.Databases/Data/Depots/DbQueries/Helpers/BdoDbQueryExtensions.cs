using BindOpen.Framework.Core.Data.Depots;
using BindOpen.Framework.Core.Data.Stores;
using BindOpen.Framework.Core.System.Diagnostics;
using System;

namespace BindOpen.Framework.Databases.Data.Depots.DbQueries
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
            Action<IBdoDbQueryDepot> action)
        {
            var depot = new BdoDbQueryDepot()
            {
                LazyLoadFunction = (IBdoDepot d, IBdoLog log) =>
                {
                    var number = 0;

                    if (d is IBdoDbQueryDepot dbQueryDepot)
                    {
                        action?.Invoke(dbQueryDepot);

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
    }
}