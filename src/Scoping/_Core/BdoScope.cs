using BindOpen.System.Data;
using BindOpen.System.Data.Assemblies;
using BindOpen.System.Data.Meta;
using BindOpen.System.Data.Stores;
using BindOpen.System.Logging;
using BindOpen.System.Scoping.Entities;
using BindOpen.System.Scoping.Script;
using BindOpen.System.Scoping.Stores;
using System;
using System.Linq;

namespace BindOpen.System.Scoping
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

            DataStore = BdoData.NewMetaSet();
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
        /// 
        /// </summary>
        /// <param key="reference"></param>
        public Type CreateType(
            IBdoClassReference reference,
            IBdoLog log = null)
        {
            if (!string.IsNullOrEmpty(reference?.DefinitionUniqueName))
            {
                var definition = ExtensionStore?.GetDefinition<IBdoEntityDefinition>(
                    reference.DefinitionUniqueName);

                return definition?.RuntimeType;
            }
            else
            {
                var assembly = AppDomain.GetAssembly(reference);
                var type = assembly.GetTypes()
                    .FirstOrDefault(q =>
                        BdoData.Class(
                            BdoData.Assembly(assembly),
                            reference.ClassName) == (BdoClassReference)reference);

                return type;
            }
        }

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
                log?.AddEvent(EventKinds.Error,
                    "Application extension missing", description: "No extension item definition store specified.");
                return false;
            }
            if (checkDataStore && DepotStore == null)
            {
                log?.AddEvent(EventKinds.Error,
                    "Depot set missing", description: "No depot set specified.");
                return false;
            }

            return true;
        }

        public IBdoScriptDomain NewScriptDomain(
            IBdoMetaSet varSet,
            IBdoScriptword scriptword = null,
            IBdoLog log = null)
        {
            return new BdoScriptDomain(this, varSet, scriptword, log);
        }

        #endregion
    }
}