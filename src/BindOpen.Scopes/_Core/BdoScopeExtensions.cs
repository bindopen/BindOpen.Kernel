using BindOpen.Extensions.Connectors;
using BindOpen.Logging;
using BindOpen.Scopes.Stores;
using System;

namespace BindOpen.Scopes
{
    /// <summary>
    /// This static class provides methods to create extension items.
    /// </summary>
    public static class BdoScopeExtensions
    {
        /// <summary>
        /// Creates a new service.
        /// </summary>
        /// <param key="scope"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Returns the log of the operation.</returns>
        public static T CreateScoped<T>(
            this IBdoScope scope)
            where T : IBdoScoped, new()
        {
            var service = new T
            {
                Scope = scope
            };

            return service;
        }

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
        public static T LoadExtensions<T>(
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

            new BdoExtensionStoreLoader(
                scope.AppDomain,
                scope.ExtensionStore,
                loadOptions)
                .LoadPackages(log);

            return scope;
        }

    }
}
