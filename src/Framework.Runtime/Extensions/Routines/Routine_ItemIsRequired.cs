using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Runtime.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using System;

namespace BindOpen.Framework.Runtime.Extensions.Routines
{
    /// <summary>
    /// This class represents a routine 'ItemIsRequired'.
    /// </summary>
    [BdoRoutine(Name = "ItemIsRequired")]
    public class Routine_ItemIsRequired : BdoRoutine
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

        #endregion

        // --------------------------------------------------
        // EXECUTION
        // --------------------------------------------------

        #region Execution

        /// <summary>
        /// Executes customly this instance.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="item">The item to use.</param>
        /// <param name="dataElement">The element to use.</param>
        /// <param name="objects">The objects to use.</param>
        /// <returns>The log of check log.</returns>
        protected override IBdoLog CustomExecute(
            IBdoScope scope = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            Object item = null,
            IDataElement dataElement = null,
            params object[] objects)
        {
            IBdoLog log = new BdoLog();

            if (dataElement == null)
                log.AddError("Element missing");
            else if (dataElement.Items.Count == 0 || dataElement.Items[0] == null)
                log.AddError("Item required").ResultCode = "ERROR_ITEMREQUIRED:" + dataElement.Key();
            else if (dataElement.ValueType.IsScalar() && dataElement.Items.Count == 1 && dataElement.GetObject().ToNotNullString() == String.Empty)
                log.AddError("Item required").ResultCode = "ERROR_ITEMREQUIRED:" + dataElement.Key();

            return log;
        }

        #endregion
    }
}
