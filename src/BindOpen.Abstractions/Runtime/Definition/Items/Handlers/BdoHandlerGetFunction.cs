using BindOpen.Data.Meta;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;
using BindOpen.Logging;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This delegate represents a handler GET function.
    /// </summary>
    /// <param name="sourceElement">The source element to consider.</param>
    /// <param name="pathDetail">The path detail to consider.</param>
    /// <param name="scope">The scope to consider.</param>
    /// <param name="varSet">The variable element set to consider.</param>
    /// <param name="alog">The log to consider.</param>
    /// <returns>Returns the target objects.</returns>
    public delegate List<object> BdoHandlerGetFunction(
            IBdoMetaData sourceElement = null,
            IBdoMetaSet pathDetail = null,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog alog = null);
}
