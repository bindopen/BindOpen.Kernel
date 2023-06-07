using BindOpen.Scoping.Data.Assemblies;
using BindOpen.Scoping.Data.Context;
using BindOpen.Scoping.Data.Meta;
using BindOpen.Scoping.Data.Stores;
using BindOpen.Logging;
using BindOpen.Scoping.Scopes.Stores;
using BindOpen.Scoping.Script;
using System;

namespace BindOpen.Scoping.Scopes
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

        IBdoScriptDomain NewScriptDomain(
            IBdoMetaSet varSet,
            IBdoScriptword scriptword = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="reference"></param>
        /// <returns></returns>
        Type CreateType(
            IBdoClassReference reference,
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