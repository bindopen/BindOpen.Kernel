using BindOpen.Logging;
using System;

namespace BindOpen.Scoping;

/// <summary>
/// This static class provides extension methods for the <see cref="IBdoScope"/> interface,
/// specifically for managing extension loading.
/// </summary>
public static class BdoScopeExtensions
{
    // Load extensions ---------------------------

    /// <summary>
    /// Loads extensions into the specified scope using the provided load options.
    /// </summary>
    /// <typeparam name="T">The type of the scope (must implement <see cref="IBdoScope"/>).</typeparam>
    /// <param name="scope">The scope to load extensions into.</param>
    /// <param name="options">The options to use for loading extensions.</param>
    /// <param name="log">The log to populate with loading events.</param>
    /// <returns>Returns True if the loading process succeeded; otherwise, False.</returns>
    public static bool LoadExtensions<T>(
        this T scope,
        IExtensionLoadOptions options,
        IBdoLog log = null)
        where T : IBdoScope
    {
        if (scope == null) return false;

        // Use the internal store loader to populate the scope's extension store
        var loader = new BdoExtensionStoreLoader(
            scope.AppDomain,
            scope.ExtensionStore,
            options);

        return loader.LoadAll(log);
    }

    /// <summary>
    /// Loads extensions into the specified scope using a configuration action for load options.
    /// </summary>
    /// <typeparam name="T">The type of the scope (must implement <see cref="IBdoScope"/>).</typeparam>
    /// <param name="scope">The scope to load extensions into.</param>
    /// <param name="loadOptionsAction">The action used to configure the load options.</param>
    /// <param name="log">The log to populate with loading events.</param>
    /// <returns>Returns True if the loading process succeeded; otherwise, False.</returns>
    public static bool LoadExtensions<T>(
        this T scope,
        Action<IExtensionLoadOptions> loadOptionsAction,
        IBdoLog log = null)
        where T : IBdoScope
    {
        if (scope == null) return false;

        IExtensionLoadOptions loadOptions = null;

        if (loadOptionsAction != null)
        {
            loadOptions = new ExtensionLoadOptions();
            loadOptionsAction.Invoke(loadOptions);
        }

        return scope.LoadExtensions(loadOptions, log);
    }
}