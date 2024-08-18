using BindOpen.Logging;
using System;

namespace BindOpen.Scoping
{
    /// <summary>
    /// This static class provides methods to create extension items.
    /// </summary>
    public static class BdoScopeExtensions
    {
        // Load extensions

        /// <summary>
        /// Loads the specified extensions.
        /// </summary>
        /// <param key="loadOptionsAction">The load options action to consider.</param>
        /// <param key="references">The extension references to consider.</param>
        public static bool LoadExtensions<T>(
            this T scope,
            IExtensionLoadOptions options,
            IBdoLog log = null)
            where T : IBdoScope
        {
            var loader = new BdoExtensionStoreLoader(
                scope.AppDomain,
                scope.ExtensionStore,
                options);

            var result = loader.LoadPackages(log);

            return result;
        }

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

            return scope.LoadExtensions(loadOptions, log);
        }
    }
}
