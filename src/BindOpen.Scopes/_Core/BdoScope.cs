using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Context;
using BindOpen.Data.Stores;
using BindOpen.Extensions.Entities;
using BindOpen.Logging;
using BindOpen.Scopes.Stores;
using BindOpen.Script;
using System;
using System.Linq;

namespace BindOpen.Scopes
{
    /// <summary>
    /// This class represents an application scope.
    /// </summary>
    public partial class BdoScope : BdoItem, IBdoScope
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

            ExtensionStore = new BdoExtensionStore();

            Context = new BdoDataContext();

            _scriptInterpreter = this.CreateInterpreter();
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
        /// The data context of this instance.
        /// </summary>
        public IBdoDataContext Context { get; set; }

        /// <summary>
        /// The data store of this instance.
        /// </summary>
        public IBdoDataStore DataStore { get; set; }

        private IBdoScriptInterpreter _scriptInterpreter;

        /// <summary>
        /// The script interpreter of this instance.
        /// </summary>
        public IBdoScriptInterpreter Interpreter => _scriptInterpreter;

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
                            reference.ClassName) == reference);

                return type;
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            Context?.Clear();
            DataStore?.Clear();
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
            bool checkDataContext = false,
            bool checkDataStore = false,
            IBdoLog log = null)
        {
            if (checkExtensionStore && ExtensionStore == null)
            {
                log?.AddError(title: "Application extension missing", description: "No extension item definition store specified.");
                return false;
            }
            if (checkDataContext && Context == null)
            {
                log?.AddError(title: "Data context missing", description: "No data context specified.");
                return false;
            }
            if (checkDataStore && DataStore == null)
            {
                log?.AddError(title: "Depot set missing", description: "No depot set specified.");
                return false;
            }

            return true;
        }

        #endregion
    }
}