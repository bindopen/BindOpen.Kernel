using BindOpen.Application.Scopes;
using BindOpen.Data.Elements;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using System;

namespace BindOpen.Extensions.Routines
{
    /// <summary>
    /// This class represents a routine 'TextFormatMustBe'.
    /// </summary>
    [BdoRoutine(Name = "TextFormatMustBe")]
    public class Routine_TextFormatMustBe : BdoRoutine
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Routine_TextFormatMustBe class.
        /// </summary>
        public Routine_TextFormatMustBe()
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
            IScriptVariableSet scriptVariableSet = null,
            Object item = null,
            IDataElement dataElement = null,
            params object[] objects)
        {
            var log = new BdoLog();

            //if (item!=null && ParameterDetail!=null)
            //{
            //    String aFormat = (ParameterDetail.GetElementItem() as string ?? "");
            //    String aString = ((item as string) ?? "");

            //    if (!string.IsNullOrEmpty(aFormat))
            //    {
            //        if (!String.Format(aString, aFormat).KeyEquals(aString))
            //        {
            //            log.AddError("Bad format").ResultCode = "ERROR_FORMAT:" + (dataElement != null ? dataElement.Key() : "");
            //        }
            //    }
            //}

            return log;
        }

        #endregion
    }

}
