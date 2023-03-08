using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Context;
using BindOpen.Data.Stores;
using BindOpen.Extensions.Entities;
using BindOpen.Logging;
using BindOpen.Scoping.Stores;
using BindOpen.Scripting;
using System;
using System.Linq;

namespace BindOpen.Scoping.Scopes
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

            _scriptInterpreter = BdoScript.CreateInterpreter(this);
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
            IBdoClassReference reference)
        {
            if (!string.IsNullOrEmpty(reference?.DefinitionUniqueName))
            {
                var definition = ExtensionStore?.GetDefinition<IBdoEntityDefinition>(
                    reference.DefinitionUniqueName);

                return definition?.RuntimeType;
            }
            else
            {
                var assembly = AppDomain.GetAssemblies()
                    .FirstOrDefault(q =>
                        BdoData.Assembly(q) == reference);
                var type = assembly.GetTypes()
                    .FirstOrDefault(q =>
                        BdoData.Class(
                            BdoData.Assembly(assembly),
                            reference.ClassName) == reference);

                return type;
            }
        }

        // Load extensions

        /// <summary>
        /// Loads the specified extensions.
        /// </summary>
        /// <param key="loadOptionsAction">The load options action to consider.</param>
        /// <param key="references">The extension references to consider.</param>
        public bool LoadExtensions(
            Action<IExtensionLoadOptions> loadOptionsAction,
            IBdoLog log = null)
        {
            var loaded = true;

            IExtensionLoadOptions loadOptions = null;
            if (loadOptionsAction != null)
            {
                loadOptions = new ExtensionLoadOptions();
                loadOptionsAction?.Invoke(loadOptions);
            }
            var loader = new BdoExtensionStoreLoader(AppDomain, ExtensionStore, loadOptions);
            loaded &= loader.LoadPackages(log);

            return loaded;
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