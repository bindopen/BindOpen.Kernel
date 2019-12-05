using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Runtime.Items
{
    /// <summary>
    /// This delegate represents a handler POST function.
    /// </summary>
    /// <param name="targetObject">The target object to consider.</param>
    /// <param name="sourceDataElement">The source data element to consider.</param>
    /// <param name="scope">The scope to consider.</param>
    /// <param name="scriptVariableSet">The script variable set to consider.</param>
    /// <param name="alog">The log to consider.</param>
    /// <returns>Returns the source object.</returns>
    public delegate List<object> BdoHandlerPostFunction(
            object targetObject,
            ref IDataElement sourceDataElement,
            IBdoScope scope = null,
            IBdoScriptVariableSet scriptVariableSet=null,
            IBdoLog alog = null);
}
