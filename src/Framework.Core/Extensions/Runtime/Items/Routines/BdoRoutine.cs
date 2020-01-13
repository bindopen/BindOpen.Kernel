using BindOpen.Framework.Application.Scopes;
using BindOpen.Framework.Data.Elements;
using BindOpen.Framework.Extensions.Definition;
using BindOpen.Framework.System.Diagnostics;
using BindOpen.Framework.System.Scripting;
using System;

namespace BindOpen.Framework.Extensions.Runtime
{
    /// <summary>
    /// This class represents a routine.
    /// </summary>
    public abstract class BdoRoutine : TBdoExtensionItem<IBdoRoutineDefinition>, IBdoRoutine
    {
        /// <summary>
        /// 
        /// </summary>
        new public IBdoRoutineConfiguration Configuration { get => base.Configuration as IBdoRoutineConfiguration; }

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Routine class.
        /// </summary>
        protected BdoRoutine() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Routine class.
        /// </summary>
        /// <param name="dto">The DTO item of this instance.</param>
        protected BdoRoutine(IBdoRoutineConfiguration dto)
        {
        }

        #endregion

        // --------------------------------------------------
        // EXECUTION
        // --------------------------------------------------

        #region Execution

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="item">The item to use.</param>
        /// <param name="dataElement">The element to use.</param>
        /// <param name="objects">The objects to use.</param>
        /// <returns>The log of check log.</returns>
        public IBdoLog Execute(
            IBdoScope scope = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            Object item = null,
            IDataElement dataElement = null,
            params object[] objects)
        {
            var log = new BdoLog();

            return log;
        }

        /// <summary>
        /// Executes customly this instance.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="item">The item to use.</param>
        /// <param name="dataElement">The element to use.</param>
        /// <param name="objects">The objects to use.</param>
        /// <returns>The log of check log.</returns>
        protected virtual IBdoLog CustomExecute(
            IBdoScope scope = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            Object item = null,
            IDataElement dataElement = null,
            params object[] objects)
        {
            return new BdoLog();
        }

        #endregion
    }
}
