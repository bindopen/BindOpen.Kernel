using BindOpen.Data.Assemblies;
using BindOpen.Data.Context;
using BindOpen.Data.Stores;
using BindOpen.Extensions.Scripting;
using BindOpen.Logging;
using BindOpen.Runtime.Stores;
using System;

namespace BindOpen.Runtime.Scopes
{
    /// <summary>
    /// This interface defines an application scope.
    /// </summary>
    public partial interface IBdoScope : IDisposable
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
        /// The data context.
        /// </summary>
        IBdoScriptInterpreter Interpreter { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="reference"></param>
        /// <returns></returns>
        Type CreateType(
            IBdoClassReference reference);

        /// <summary>
        /// Loads the specified extensions.
        /// </summary>
        /// <param key="references">The extension references to consider.</param>
        /// <param key="log"></param>
        bool LoadExtensions(
            IBdoAssemblyReference[] references,
            IBdoLog log = null);

        /// <summary>
        /// Loads the specified extensions.
        /// </summary>
        /// <param key="references">The extension references to consider.</param>
        bool LoadExtensions(
            params IBdoAssemblyReference[] references);

        /// <summary>
        /// Loads the specified extensions.
        /// </summary>
        /// <param key="loadOptionsAction">The load options action to consider.</param>
        /// <param key="references">The extension references to consider.</param>
        /// <param key="log"></param>
        bool LoadExtensions(
            Func<IExtensionLoadOptions, bool> loadOptionsAction,
            IBdoAssemblyReference[] references,
            IBdoLog log = null);

        /// <summary>
        /// Loads the specified extensions.
        /// </summary>
        /// <param key="loadOptionsAction">The load options action to consider.</param>
        /// <param key="references">The extension references to consider.</param>
        bool LoadExtensions(
            Func<IExtensionLoadOptions, bool> loadOptionsAction,
            params IBdoAssemblyReference[] references);

        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();
    }
}