using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Context;
using BindOpen.Data.Items;
using BindOpen.Data.Stores;
using BindOpen.Extensions.Scripting;
using BindOpen.Logging;
using BindOpen.Runtime.Definition;
using BindOpen.Runtime.Stores;
using System;
using System.Linq;

namespace BindOpen.Runtime.Scopes
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
        /// <param name="appDomain">The application domain to instance.</param>
        public BdoScope(AppDomain appDomain = null) : base()
        {
            AppDomain = appDomain ?? AppDomain.CurrentDomain;

            ExtensionStore = new BdoExtensionStore();

            Context = new BdoDataContext();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reference"></param>
        public Type CreateType(
            IBdoClassReference reference)
        {
            if (!string.IsNullOrEmpty(reference?.DefinitionUniqueName))
            {
                var definition = ExtensionStore?.GetItemDefinitionWithUniqueName<IBdoEntityDefinition>(
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

        /// <summary>
        /// Creates a new script interpreter.
        /// </summary>
        /// <returns>Returns the new script interpreter.</returns>
        public IBdoScriptInterpreter NewScriptInterpreter()
            => BdoScript.CreateInterpreter(this);

        // Load extensions

        /// <summary>
        /// Loads the specified extensions.
        /// </summary>
        /// <param name="loadOptionsAction">The load options action to consider.</param>
        /// <param name="references">The extension references to consider.</param>
        public bool LoadExtensions(
            Func<IExtensionLoadOptions, bool> loadOptionsAction,
            IBdoAssemblyReference[] references,
            IBdoLog log = null)
        {
            var loaded = true;

            IExtensionLoadOptions loadOptions = null;
            if (loadOptionsAction != null)
            {
                loadOptions = new ExtensionLoadOptions();
                loaded &= loadOptionsAction?.Invoke(loadOptions) ?? true;
            }
            var loader = new BdoExtensionStoreLoader(AppDomain, ExtensionStore, loadOptions);
            loaded &= loader.LoadExtensionsInStore(references, log);

            return loaded;
        }

        /// <summary>
        /// Loads the specified extensions.
        /// </summary>
        /// <param name="loadOptionsAction">The load options action to consider.</param>
        /// <param name="references">The extension references to consider.</param>
        public bool LoadExtensions(
            Func<IExtensionLoadOptions, bool> loadOptionsAction,
            params IBdoAssemblyReference[] references)
            => LoadExtensions(loadOptionsAction, references, null);

        /// <summary>
        /// Loads the specified extensions.
        /// </summary>
        /// <param name="references">The extension references to consider.</param>
        public bool LoadExtensions(
            IBdoAssemblyReference[] references,
            IBdoLog log = null)
            => LoadExtensions(null, references, log);

        /// <summary>
        /// Loads the specified extensions.
        /// </summary>
        /// <param name="loadOptionsAction">The load options action to consider.</param>
        /// <param name="references">The extension references to consider.</param>
        public bool LoadExtensions(
            params IBdoAssemblyReference[] references)
            => LoadExtensions(null, references, null);

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            Context?.Clear();
            DataStore?.Clear();
            ExtensionStore?.Clear();
        }

        #endregion
    }
}