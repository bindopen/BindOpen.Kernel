﻿using BindOpen.Data.Meta;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines an named data item.
    /// </summary>
    public static class IBdoConfigurableExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        public static T WithConfig<T>(
            this T obj,
            IBdoConfiguration config)
            where T : IBdoConfigurable
        {
            if (obj != null)
            {
                obj.Config = config;
            }

            return obj;
        }
    }
}