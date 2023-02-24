﻿using BindOpen.Data.Configuration;

namespace BindOpen.Data
{
    /// <summary>
    /// This static class provides methods to handle configs.
    /// </summary>
    public partial class BdoConfig
    {
        /// <summary>
        /// Instantiates a new instance of the BdoConfigurationSet class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        public static BdoConfigurationSet NewList(
            params IBdoConfiguration[] items)
        {
            var config = BdoData.NewSet<BdoConfigurationSet, IBdoConfiguration>(items);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static T NewList<T>(
            params IBdoConfiguration[] items)
            where T : BdoConfigurationSet, new()
        {
            var list = new T();
            list.With(items);
            return list;
        }
    }
}