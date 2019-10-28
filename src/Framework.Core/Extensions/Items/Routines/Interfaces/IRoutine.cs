using System;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Extensions.Items.Routines;
using BindOpen.Framework.Core.Extensions.Items.Routines.Definition;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Items.Routines
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRoutine : ITAppExtensionItem<IRoutineDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appScope"></param>
        /// <param name="scriptVariableSet"></param>
        /// <param name="item"></param>
        /// <param name="dataElement"></param>
        /// <param name="objects"></param>
        /// <returns></returns>
        ILog Execute(
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            Object item = null,
            IDataElement dataElement = null,
            params object[] objects);
    }
}