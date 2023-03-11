using BindOpen.Data.Assemblies;
using BindOpen.Data.Context;
using BindOpen.Data.Stores;
using BindOpen.Logging;
using BindOpen.Scopes.Stores;
using BindOpen.Script;
using System;

namespace BindOpen.Scopes.Scopes
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
        /// <param key="loadOptionsAction">The load options action to consider.</param>
        /// <param key="references">The extension references to consider.</param>
        /// <param key="log"></param>
        bool LoadExtensions(
            Action<IExtensionLoadOptions> loadOptionsAction,
            IBdoLog log = null);

        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="checkExtensionStore"></param>
        /// <param name="checkDataContext"></param>
        /// <param name="checkDataStore"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        bool Check(
            bool checkExtensionStore = false,
            bool checkDataContext = false,
            bool checkDataStore = false,
            IBdoLog log = null);
    }
}