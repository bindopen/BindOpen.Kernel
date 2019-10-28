using System;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// This static class extends .Net core dependency injection namespace.
    /// </summary>
    public static class BdoConfigurationBinder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetBdoValue<T>(this IConfiguration configuration, string key)
        {
            if (configuration == default) return default;
            return configuration.GetValue<T>(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetBdoValue<T>(this IConfiguration configuration, string key, T defaultValue)
        {
            if (configuration == default) return default;
            return configuration.GetValue<T>(key, defaultValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetBdoValue(this IConfiguration configuration, Type type, string key)
        {
            if (configuration == default) return default;
            return configuration.GetValue(type, key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static object GetBdoValue(this IConfiguration configuration, Type type, string key, object defaultValue)
        {
            if (configuration == default) return default;
            return configuration.GetValue(type, key, defaultValue);
        }
    }
}