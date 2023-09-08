using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Data.Stores;
using BindOpen.Kernel.Logging;
using BindOpen.Kernel.Scoping.Script;
using BindOpen.Kernel.Scoping;
using System;

namespace BindOpen.Kernel.Scoping
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
        /// The depot store of this instance.
        /// </summary>
        IBdoDepotStore DepotStore { get; }

        /// <summary>
        /// The data store of this instance.
        /// </summary>
        IBdoMetaSet DataStore { get; }

        /// <summary>
        /// The data context.
        /// </summary>
        IBdoScriptInterpreter Interpreter { get; }

        IBdoScriptDomain NewScriptDomain(
            IBdoMetaSet varSet,
            IBdoScriptword scriptword = null,
            IBdoLog log = null);

        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="checkExtensionStore"></param>
        /// <param name="checkDataStore"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        bool Check(
            bool checkExtensionStore = false,
            bool checkDataStore = false,
            IBdoLog log = null);
    }
}