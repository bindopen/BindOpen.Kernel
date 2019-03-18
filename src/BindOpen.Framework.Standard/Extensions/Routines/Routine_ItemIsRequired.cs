using System;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Extensions.Runtime.Routines;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Standard.Extensions.Routines
{
    /// <summary>
    /// This class represents a routine 'ItemIsRequired'.
    /// </summary>
    public class Routine_ItemIsRequired : Routine
   {

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Routine_ItemIsRequired class.
        /// </summary>
        public Routine_ItemIsRequired()
        {
        }

        ///// <summary>
        ///// Instantiates a new instance of the Routine_ItemIsRequired class.
        ///// </summary>
        ///// <param name="name">The name to consider.</param>
        ///// <param name="dataSource">The Odbc database data source to consider.</param>
        ///// <param name="appScope">The application scope to consider.</param>
        ///// <param name="log">The log of creation.</param>
        //public Routine_ItemIsRequired(
        //    String name) : base(name, dataSource, appScope, log)
        //{
        //}

        #endregion


        // --------------------------------------------------
        // EXECUTION
        // --------------------------------------------------

        #region Execution

        /// <summary>
        /// Executes customly this instance.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="item">The item to use.</param>
        /// <param name="dataElement">The element to use.</param>
        /// <param name="objects">The objects to use.</param>
        /// <returns>The log of check log.</returns>
        protected override Log CustomExecute(
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null,
            Object item = null,
            DataElement dataElement = null,
            params Object[] objects)
        {
            Log log = new Log();

            if (dataElement == null)
                log.AddError("Element missing");
            else if (dataElement.Items.Count == 0 || dataElement.Items[0] == null)
                log.AddError("Item required").ResultCode = "ERROR_ITEMREQUIRED:" + dataElement.Key();
            else if (dataElement.ValueType.IsScalar() && dataElement.Items.Count == 1 && dataElement.GetItem(0).ToNotNullString() == String.Empty)
                    log.AddError("Item required").ResultCode = "ERROR_ITEMREQUIRED:" + dataElement.Key();

            return log;
        }

        #endregion

    }

}
