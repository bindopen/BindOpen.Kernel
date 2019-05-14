using System;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// This static class extends .Net core dependency injection namespace.
    /// </summary>
    public static class BdoConfigurationBinder
    {
        public static T GetBdoValue<T>(this IConfiguration configuration, string key)
        {
            if (configuration == default) return default;
            return configuration.GetValue<T>(key);
        }

        public static T GetBdoValue<T>(this IConfiguration configuration, string key, T defaultValue)
        {
            if (configuration == default) return default;
            return configuration.GetValue<T>(key, defaultValue);
        }

        public static object GetBdoValue(this IConfiguration configuration, Type type, string key)
        {
            if (configuration == default) return default;
            return configuration.GetValue(type, key);
        }

        public static object GetBdoValue(this IConfiguration configuration, Type type, string key, object defaultValue)
        {
            if (configuration == default) return default;
            return configuration.GetValue(type, key, defaultValue);
        }
    }
}