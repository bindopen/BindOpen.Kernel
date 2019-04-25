using System;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Extensions.Definitions.Routines;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Items.Routines
{
    /// <summary>
    /// This class represents a routine.
    /// </summary>
    public abstract class Routine : TAppExtensionItem<IRoutineDefinition>, IRoutine
    {
        new public IRoutineConfiguration Configuration { get => base.Configuration as IRoutineConfiguration; }

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Routine class.
        /// </summary>
        protected Routine() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Routine class.
        /// </summary>
        /// <param name="dto">The DTO item of this instance.</param>
        protected Routine(IRoutineConfiguration dto)
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="item">The item to use.</param>
        /// <param name="dataElement">The element to use.</param>
        /// <param name="objects">The objects to use.</param>
        /// <returns>The log of check log.</returns>
        public ILog Execute(
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            Object item = null,
            IDataElement dataElement = null,
            params object[] objects)
        {
            ILog log = new Log();

            //log.AddEvents(appScope.Check(
            //    isAppExtensionChecked: !string.IsNullOrEmpty(this.DefinitionUniqueId),
            //    isScriptInterpreterChecked: this.CommandSet.Items.Any(p => p.Kind == CommandKind.Script)));
            ////,
            ////    isConnectionManagerChecked: (dataElement!=null)&& (dataElement.ItemReference!=null)&& (dataElement.ItemReference.GetDataSourceKind() != DataSourceKind.None )));
            
            //if (!log.HasErrorsOrExceptions())
            //{
            //    scriptVariableSet = (scriptVariableSet ?? new ScriptVariableSet());

            //    // we launch the runtime check (the one defined in code)
            //    log.AddEvents(this.CustomExecute(appScope, scriptVariableSet, item, dataElement, objects));

            //    // then we launch commands
            //    foreach (Command command in this.CommandSet.Items.Where(p => p.Kind != CommandKind.Script))
            //    {
            //        string result = "";
            //        if (!log.Append(command.ExecuteWithResult(out result)).HasErrorsOrExceptions())
            //            scriptVariableSet.SetValue("command_result$" + command.Key(), result);
            //    }

            //    // then we determine the result
            //    foreach (ConditionalEvent currentConditionalEvent in this.OutputEventSet.Items)
            //        if (appScope.ScriptInterpreter.Evaluate(
            //            currentConditionalEvent.ConditionScript, scriptVariableSet, log) as Boolean? == true)
            //        {
            //            log.AddEvent(new LogEvent(currentConditionalEvent));
            //            break;
            //        }
            //}

            return log;
        }

        /// <summary>
        /// Executes customly this instance.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="item">The item to use.</param>
        /// <param name="dataElement">The element to use.</param>
        /// <param name="objects">The objects to use.</param>
        /// <returns>The log of check log.</returns>
        protected virtual ILog CustomExecute(
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            Object item = null,
            IDataElement dataElement = null,
            params object[] objects)
        {
            return new Log();
        }

        #endregion
    }
}
