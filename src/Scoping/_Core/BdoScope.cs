using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Stores;
using BindOpen.Logging;
using BindOpen.Scoping.Script;
using System;

namespace BindOpen.Scoping
{
    /// <summary>
    /// This class represents an application scope.
    /// </summary>
    public partial class BdoScope : BdoObject, IBdoScope
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoScope class.
        /// </summary>
        /// <param key="appDomain">The application domain to instance.</param>
        public BdoScope(AppDomain appDomain = null) : base()
        {
            AppDomain = appDomain ?? AppDomain.CurrentDomain;

            ExtensionStore = BdoData.New<BdoExtensionStore>();

            Interpreter = BdoScript.NewInterpreter(this);

            DepotStore = BdoData.NewDepotStore();

            DataStore = BdoData.NewSet();
        }

        #endregion

        // ------------------------------------------
        // IBdoScope Implementation
        // ------------------------------------------

        #region IBdoScope

        /// <summary>
        /// The application domain.
        /// </summary>
        public AppDomain AppDomain { get; } = null;

        /// <summary>
        /// The BindOpen extension store of this instance.
        /// </summary>
        public IBdoExtensionStore ExtensionStore { get; set; }

        /// <summary>
        /// The data store of this instance.
        /// </summary>
        public IBdoDepotStore DepotStore { get; set; }

        /// <summary>
        /// The data store of this instance.
        /// </summary>
        public IBdoMetaSet DataStore { get; }

        public IBdoScriptInterpreter Interpreter { get; }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            DepotStore?.Clear();
            ExtensionStore?.Clear();
        }

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param key="checkExtensionStore">Indicates whether the extension item definition store extistence is chekced.</param>
        /// <param key="checkDataContext">Indicates whether the data context extistence is chekced.</param>
        /// <param key="checkDataStore">Indicates whether the data store extistence is chekced.</param>
        /// <returns>The log of check log.</returns>
        public bool Check(
            bool checkExtensionStore = false,
            bool checkDataStore = false,
            IBdoLog log = null)
        {
            if (checkExtensionStore && ExtensionStore == null)
            {
                log?.AddEvent(BdoEventKinds.Error,
                    "Application extension missing", description: "No extension item definition store specified.");
                return false;
            }
            if (checkDataStore && DepotStore == null)
            {
                log?.AddEvent(BdoEventKinds.Error,
                    "Depot set missing", description: "No depot set specified.");
                return false;
            }

            return true;
        }

        public IBdoScriptDomain NewScriptDomain(
            IBdoMetaSet metaSet,
            IBdoScriptword scriptword = null,
            IBdoLog log = null)
        {
            return new BdoScriptDomain(this, metaSet, scriptword, log);
        }

        #endregion
    }
}