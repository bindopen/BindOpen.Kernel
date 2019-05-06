using System;
using BindOpen.Framework.Core.Application.Depots.Datasources;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Context;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions;
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
        public IAppExtension Extension { get; set; } = null;

        /// <summary>
        /// The script interpreter of this instance.
        /// </summary>
        public IScriptInterpreter Interpreter { get; set; } = null;

        /// <summary>
        /// The data context of this instance.
        /// </summary>
        public IDataContext Context { get; set; } = null;

        /// <summary>
        /// The data source service of this instance.
        /// </summary>
        public IDataSourceDepot DataSourceDepot { get; set; } = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the AppScope class.
        /// </summary>
        /// <param name="appDomain">The application domain to consider.</param>
        public AppScope(
            AppDomain appDomain = null,
            IDataContext context = null,
            IScriptInterpreter interpreter =null,
            IDataSourceDepot dataSourceDepot = null) :  base()
        {
            Initialize(appDomain ?? AppDomain.CurrentDomain);

            Context = context ?? new DataContext();
            Interpreter = interpreter ?? new ScriptInterpreter(this);
            DataSourceDepot = dataSourceDepot ?? new DataSourceDepot();
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
        public virtual void Initialize(AppDomain appDomain = null)
        {
            if (appDomain != null)
            {
                Extension = new AppExtension(appDomain);
            }

            Context = new DataContext();
            DataSourceDepot = new DataSourceDepot();
            Interpreter = new ScriptInterpreter(this);
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
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override ILog Update<T>(
            T item = default,
            string[] specificationAreas = null,
            UpdateMode[] updateModes = null)
        {
            Extension?.Initialize();

            return new Log();
        }

        #endregion
    }
}