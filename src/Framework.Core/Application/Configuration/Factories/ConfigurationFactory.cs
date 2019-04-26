using System;

namespace BindOpen.Framework.Core.Application.Configuration
{
    /// <summary>
    /// This static class provides methods to create configurations.
    /// </summary>
    public static class ConfigurationFactory
    {
        /// <summary>
        /// Creates the instance of the specified configuration.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <returns>Returns the configuration.</returns>
        public static T CreateConfiguration<T>(ConfigurationDto item) where T : Configuration, new()
        {
            T config = Activator.CreateInstance(typeof(T), new object[] { item }) as T;

            // we update setters and getters to bind item

            return config;
        }
    }
}
