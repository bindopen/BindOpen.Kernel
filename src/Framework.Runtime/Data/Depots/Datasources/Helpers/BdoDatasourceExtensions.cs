using BindOpen.Framework.Core.Data.Depots;
using BindOpen.Framework.Core.Data.Items.Datasources;
using BindOpen.Framework.Core.Data.Stores;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Runtime.Application.Options.Hosts;
using BindOpen.Framework.Runtime.Data.Depots.Datasources;
using System;

namespace BindOpen.Framework.Core.Data.Depots.Datasources
{
    /// <summary>
    /// This class represents an data source extensions.
    /// </summary>
    public static class BdoDatasourceExtensions
    {
        /// <summary>
        /// Add a data source depot into the specified data store.
        /// </summary>
        /// <param name="dataStore">The data store to consider.</param>
        /// <returns>Returns the data store to update.</returns>
        public static IBdoDataStore RegisterDasourceDepot(this IBdoDataStore dataStore, IBdoHostOptions options = null)
        {
            var depot = new BdoDatasourceDepot()
            {
                LazyLoadFunction = (IBdoDepot d, IBdoLog log) =>
                {
                    var dataSourceNumber = 0;

                    if (d is BdoDatasourceDepot datasourceDepot)
                    {
                        if (options?.HostSettings?.HostConfiguration?.Datasources != null)
                        {
                            foreach (Datasource dataSource in options?.HostSettings?.HostConfiguration?.Datasources)
                            {
                                datasourceDepot.Add(dataSource);
                            }
                            dataSourceNumber++;
                        }

                        if (!log.HasErrorsOrExceptions())
                        {
                            log.AddMessage("Depot loaded (" + dataSourceNumber + " data sources added)");
                        }
                    }

                    return dataSourceNumber;
                }
            };

            // we populate the data source depot from settings

            dataStore?.Add<IBdoDatasourceDepot>(depot);
            return dataStore;
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
    }
}