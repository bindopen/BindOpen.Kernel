using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Runtime.Handlers
{
    /// <summary>
    /// This delegate represents a handler GET function.
    /// </summary>
    /// <param name="sourceElement">The source element to consider.</param>
    /// <param name="pathDetail">The path detail to consider.</param>
    /// <param name="appScope">The application scope to consider.</param>
    /// <param name="scriptVariableSet">The script variable set to consider.</param>
    /// <param name="alog">The log to consider.</param>
    /// <returns>Returns the target objects.</returns>
    public delegate List<object> HandlerGetFunction(
            IDataElement sourceElement = null,
            IDataElementSet pathDetail = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog alog = null);
}
