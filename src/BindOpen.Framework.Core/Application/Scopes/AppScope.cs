using System;
using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Datasources;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Context;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Configuration;
using BindOpen.Framework.Core.Extensions.Definition;
using BindOpen.Framework.Core.Extensions.Runtime;
using BindOpen.Framework.Core.System.Assemblies;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Application.Scopes
{
    /// <summary>
    /// This class represents an application scope.
    /// </summary>
    public class AppScope : DataItem, IAppScope
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The application extension of this instance.
        /// </summary>
        public AppExtension AppExtension { get; set; } = null;

        /// <summary>
        /// The script interpreter of this instance.
        /// </summary>
        public ScriptInterpreter ScriptInterpreter { get; set; } = null;

        /// <summary>
        /// The data context of this instance.
        /// </summary>
        public DataContext DataContext { get; set; } = null;

        /// <summary>
        /// The data source service of this instance.
        /// </summary>
        public DataSourceService DataSourceService { get; set; } = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the AppScope class.
        /// </summary>
        public AppScope() : this(AppDomain.CurrentDomain)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the AppScope class.
        /// </summary>
        /// <param name="appDomain">The application domain to consider.</param>
        public AppScope(AppDomain appDomain)
        {
            this.SetAppDomain(appDomain);

            this.DataContext = new DataContext();
            this.ScriptInterpreter = new ScriptInterpreter(this);
            this.DataSourceService = new DataSourceService();
        }

        /// <summary>
        /// Instantiates a new instance of the AppScope class.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        public AppScope(IAppScope appScope) : this(AppDomain.CurrentDomain)
        {
            this.AppExtension = appScope?.AppExtension;
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        // Items --------------------------------------

        /// <summary>
        /// Creates the instance of the specified extension item.
        /// </summary>
        /// <param name="definitionName">The unique name of the definition to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="name">The name to consider.</param>
        /// <param name="log">The log to consider.</param>
        public ITAppExtensionRuntimeItem<T> CreateItem<T>(
            String definitionName,
            TAppExtensionItemConfiguration<T> configuration,
            String name = null,
            Log log = null) where T : AppExtensionItemDefinition
        {
            if (this.Check(true).HasErrorsOrExceptions())
                return null;

            if (definitionName == null && configuration != null)
                definitionName = configuration.DefinitionUniqueId;

            AppExtensionItemKind extensionItemKind = typeof(T).GetExtensionItemKind();
            AssemblyHelper.ClassReference assemblyReference =
                this.AppExtension.GetItemClassReference(extensionItemKind, definitionName, out AppExtensionItemDefinition definitionObject);
            T definition = definitionObject as T;

            if (definition == null)
            {
                log?.AddError("Could not retrieve the extension item '" + definitionName + "' of kind '" + extensionItemKind.ToString() + "'");
            }

            Object extensionObject = null;
            log?.AddEvents(this.AppExtension.CreateInstance(assemblyReference, out extensionObject, name, configuration, this));

            ITAppExtensionRuntimeItem<T> extensionItem = extensionObject as ITAppExtensionRuntimeItem<T>;

            if (extensionItem == null)
            {
                log?.AddError("Could not create '" + extensionItemKind.ToString() + "' object with unique name '" + definitionName + "'");
            }
            else
            {
                if (configuration == null)
                {
                    configuration = this.AppExtension.CreateConfiguration<T>(definitionName, null, log);
                }
                else
                {
                    configuration.DefinitionUniqueId = definitionName;
                    configuration.SetDefinition(definition);
                }

                extensionItem.SetConfiguration(configuration);
            }

            return extensionItem;
        }

        // Viewer ---------------------------------------

        /// <summary>
        /// Creates the instance of the viewer of the specified extension object class.
        /// </summary>
        /// <param name="libraryItemKind">The extension item kind to consider.</param>
        /// <param name="definitionName">The definition name to consider.</param>
        /// <param name="viewerKey">The viewer key to consider.</param>
        /// <param name="log">The log to consider.</param>
        public Object CreateViewer(
            AppExtensionItemKind libraryItemKind,
            String definitionName,
            String viewerKey,
            Log log = null)
        {
            if (this.Check(true).HasErrorsOrExceptions())
                return null;

            AssemblyHelper.ClassReference assemblyReference =
                this.AppExtension.GetViewerClassReference(libraryItemKind, definitionName, viewerKey);

            log.AddEvents(this.AppExtension.CreateInstance(assemblyReference, out object object1));
            return object1;
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the specified application domain.
        /// </summary>
        /// <param name="appDomain">The application domain to instance.</param>
        public virtual void SetAppDomain(AppDomain appDomain)
        {
            if (appDomain != null)
            {
                this.AppExtension = new AppExtension(appDomain);

                this.DataContext = new DataContext();
                this.ScriptInterpreter = new ScriptInterpreter(this);
                this.DataSourceService = new DataSourceService();
            }
        }

        #endregion

        // --------------------------------------------------
        // UPDATE, CHECK, REPAIR
        // --------------------------------------------------

        #region Update_Check_Repair

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override Log Update<T>(
            T item = null,
            List<String> specificationAreas = null,
            List<UpdateMode> updateModes = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            this.AppExtension?.Initialize();
            //if (this._DataContext != null)
            //    this._DataContext.LoadExtensions(this._AppExtension);
            this.ScriptInterpreter?.LoadDefinitions();

            return new Log();
        }

        #endregion
    }
}