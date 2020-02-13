using BindOpen.Framework.Application.Scopes;
using BindOpen.Framework.Data.Elements;
using BindOpen.Framework.Extensions.Definition;
using BindOpen.Framework.System.Diagnostics;
using BindOpen.Framework.System.Scripting;
using System;

namespace BindOpen.Framework.Extensions.Runtime
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoRoutine : ITBdoExtensionItem<IBdoRoutineDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="item"></param>
        /// <param name="dataElement"></param>
        /// <param name="objects"></param>
        /// <returns></returns>
        IBdoLog Execute(
            IBdoScope scope = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            Object item = null,
            IDataElement dataElement = null,
            params object[] objects);
    }
}