using BindOpen.Framework.Core.Application.Depots;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Context;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.References;
using BindOpen.Framework.Core.Extensions.Runtime.Stores;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using System;

namespace BindOpen.Framework.Core.Application.Scopes
{
    /// <summary>
    /// This class represents an application scope.
    /// </summary>
    public class BdoScope : DataItem, IBdoScope
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The application domain.
        /// </summary>
        public AppDomain AppDomain { get; } = null;

        /// <summary>
        /// The BindOpen extension store of this instance.
        /// </summary>
        public IBdoExtensionStore ExtensionStore { get; set; } = null;

        /// <summary>
        /// The script interpreter of this instance.
        /// </summary>
        public IBdoScriptInterpreter Interpreter { get; set; } = null;

        /// <summary>
        /// The data context of this instance.
        /// </summary>
        public IBdoDataContext Context { get; set; } = null;

        /// <summary>
        /// The set of depots of this instance.
        /// </summary>
        public IBdoDepotSet DepotSet { get; set; } = null;

        #endregion

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
            Interpreter = new BdoScriptInterpreter(this);

            DepotSet = new BdoDepotSet();
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="isExtensionStoreChecked">Indicates whether the extension item definition store extistence is chekced.</param>
        /// <param name="isScriptInterpreterChecked">Indicates whether the script interpreter extistence is chekced.</param>
        /// <param name="isDataContextChecked">Indicates whether the data context extistence is chekced.</param>
        /// <param name="isDepotSetChecked">Indicates whether the depot set extistence is chekced.</param>
        /// <returns>The log of check log.</returns>
        public IBdoLog Check(
            bool isExtensionStoreChecked = false,
            bool isScriptInterpreterChecked = false,
            bool isDataContextChecked = false,
            bool isDepotSetChecked = false)
        {
            IBdoLog log = new BdoLog();

            if (isExtensionStoreChecked && ExtensionStore == null)
                log.AddError(title: "Application extension missing", description: "No extension item definition store specified.");
            if (isScriptInterpreterChecked && Interpreter == null)
                log.AddError(title: "Script interpreter missing", description: "No script interpreter specified.");
            if (isDataContextChecked && Context == null)
                log.AddError(title: "Data context missing", description: "No data context specified.");
            if (isDepotSetChecked && DepotSet == null)
                log.AddError(title: "Depot set missing", description: "No depot set specified.");

            return log;
        }

        #endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Loads the specified extensions.
        /// </summary>
        /// <param name="loadOptionsAction">The load options action to consider.</param>
        /// <param name="references">The extension references to consider.</param>
        public IBdoLog LoadExtensions(
            Action<IExtensionLoadOptions> loadOptionsAction,
            params IBdoExtensionReference[] references)
        {
            IExtensionLoadOptions loadOptions = null;
            if (loadOptionsAction != null)
            {
                loadOptions = new ExtensionLoadOptions();
                loadOptionsAction?.Invoke(loadOptions);
            }
            var loader = new BdoExtensionStoreLoader(AppDomain, ExtensionStore, loadOptions);
            var log = loader.LoadExtensionsInStore(references);
            return log;
        }

        /// <summary>
        /// Loads the specified extensions.
        /// </summary>
        /// <param name="references">The extension references to consider.</param>
        public IBdoLog LoadExtensions(params IBdoExtensionReference[] references)
            => LoadExtensions(null, references);

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
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override IBdoLog Update<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateModes[] updateModes = null)
        {
            return new BdoLog();
        }

        #endregion
    }
}