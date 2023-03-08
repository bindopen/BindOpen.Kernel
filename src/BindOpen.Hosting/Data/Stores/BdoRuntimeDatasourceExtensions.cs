﻿using BindOpen.Data;
using BindOpen.Data.Stores;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// This class represents a runtime data source extensions.
    /// </summary>
    public static class BdoScopingDatasourceExtensions
    {
        /// <summary>
        /// Adds the specified source.
        /// </summary>
        /// <param key="depot">The depot to consider.</param>
        /// <param key="datasource">The datasource to consider.</param>
        public static IBdoSourceDepot AddDatasource(
            this IBdoSourceDepot depot,
            BdoDatasource datasource)
        {
            depot?.Add(datasource);

            return depot;
        }
    }
}