using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using System;
using System.Collections.Generic;

namespace BindOpen.Framework.Core.Extensions.Runtime.Handlers
{

    /// <summary>
    /// This delegate represents a handler POST function.
    /// </summary>
    /// <param name="aTargetObject">The target object to consider.</param>
    /// <param name="aSourceDataElement">The source data element to consider.</param>
    /// <param name="appScope">The application scope to consider.</param>
    /// <param name="scriptVariableSet">The script variable set to consider.</param>
    /// <param name="alog">The log to consider.</param>
    /// <returns>Returns the source object.</returns>
    public delegate List<Object> HandlerPostFunction(
            Object aTargetObject,
            ref DataElement aSourceDataElement,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet=null,
            Log alog = null);

}
