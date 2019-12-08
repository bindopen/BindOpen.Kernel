using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using System.Collections.Generic;

namespace BindOpen.Framework.Core.Extensions.Runtime.Items
{
    /// <summary>
    /// This delegate represents a handler GET function.
    /// </summary>
    /// <param name="sourceElement">The source element to consider.</param>
    /// <param name="pathDetail">The path detail to consider.</param>
    /// <param name="scope">The scope to consider.</param>
    /// <param name="scriptVariableSet">The script variable set to consider.</param>
    /// <param name="alog">The log to consider.</param>
    /// <returns>Returns the target objects.</returns>
    public delegate List<object> BdoHandlerGetFunction(
            IDataElement sourceElement = null,
            IDataElementSet pathDetail = null,
            IBdoScope scope = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog alog = null);
}
