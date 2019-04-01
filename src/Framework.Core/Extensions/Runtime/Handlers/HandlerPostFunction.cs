using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Runtime.Handlers
{
    /// <summary>
    /// This delegate represents a handler POST function.
    /// </summary>
    /// <param name="targetObject">The target object to consider.</param>
    /// <param name="sourceDataElement">The source data element to consider.</param>
    /// <param name="appScope">The application scope to consider.</param>
    /// <param name="scriptVariableSet">The script variable set to consider.</param>
    /// <param name="alog">The log to consider.</param>
    /// <returns>Returns the source object.</returns>
    public delegate List<object> HandlerPostFunction(
            object targetObject,
            ref IDataElement sourceDataElement,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet=null,
            ILog alog = null);
}
