using BindOpen.System.Logging;
using BindOpen.System.Scoping;
using BindOpen.System.Scoping.Stores;
using System;

namespace BindOpen.System.Scoping
{
    /// <summary>
    /// This static class provides methods to create extension items.
    /// </summary>
    public static class BdoScopeExtensions
    {
        /// <summary>
        /// Creates a new connected service.
        /// </summary>
        /// <param key="scope"></param>
        /// <param key="connector"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Returns the log of the operation.</returns>
        public static T CreateConnected<T>(
            this IBdoScope scope,
            IBdoConnector connector)
            where T : IBdoConnected, new()
        {
            var service = scope.CreateScoped<T>();

            service.WithConnector(connector);

            return service;
        }

        // Load extensions

        /// <summary>
        /// Loads the specified extensions.
        /// </summary>
        /// <param key="loadOptionsAction">The load options action to consider.</param>
        /// <param key="references">The extension references to consider.</param>
        public static bool LoadExtensions<T>(
            this T scope,
            Action<IExtensionLoadOptions> loadOptionsAction,
            IBdoLog log = null)
            where T : IBdoScope
        {
            IExtensionLoadOptions loadOptions = null;

            if (loadOptionsAction != null)
            {
                loadOptions = new ExtensionLoadOptions();
                loadOptionsAction?.Invoke(loadOptions);
            }

            var loader = new BdoExtensionStoreLoader(
                scope.AppDomain,
                scope.ExtensionStore,
                loadOptions);


            var result = loader.LoadPackages(log);

            return result;
        }

    }
}
