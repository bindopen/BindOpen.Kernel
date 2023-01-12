using BindOpen.Extensions.Scripting;
using BindOpen.Meta.Context;
using BindOpen.Meta.Stores;
using BindOpen.Runtime.References;
using BindOpen.Runtime.Stores;
using System;
using BindOpen.Logging;

namespace BindOpen.Runtime.Scopes
{
    /// <summary>
    /// This interface defines an application scope.
    /// </summary>
    public interface IBdoScope : IDisposable
    {
        /// <summary>
        /// The application domain.
        /// </summary>
        AppDomain AppDomain { get; }

        /// <summary>
        /// The extension item definition store.
        /// </summary>
        IBdoExtensionStore ExtensionStore { get; }

        /// <summary>
        /// The data store of this instance.
        /// </summary>
        IBdoDataStore DataStore { get; }

        /// <summary>
        /// The data context.
        /// </summary>
        IBdoDataContext Context { get; }

        /// <summary>
        /// Creates a new script interpreter.
        /// </summary>
        /// <returns>Returns the new script interpreter.</returns>
        IBdoScriptInterpreter NewScriptInterpreter();

        /// <summary>
        /// Loads the specified extensions.
        /// </summary>
        /// <param name="references">The extension references to consider.</param>
        /// <param name="log"></param>
        bool LoadExtensions(
            IBdoExtensionReference[] references,
            IBdoLog log = null);

        /// <summary>
        /// Loads the specified extensions.
        /// </summary>
        /// <param name="loadOptionsAction">The load options action to consider.</param>
        /// <param name="references">The extension references to consider.</param>
        /// <param name="log"></param>
        bool LoadExtensions(
            Func<IExtensionLoadOptions, bool> loadOptionsAction,
            IBdoExtensionReference[] references,
            IBdoLog log = null);

        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();
    }
}