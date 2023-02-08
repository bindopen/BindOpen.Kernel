using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This delegate represents a handler POST function.
    /// </summary>
    /// <param name="targetObject">The target object to consider.</param>
    /// <param name="sourceBdoElement">The source data element to consider.</param>
    /// <param name="scope">The scope to consider.</param>
    /// <param name="varSet">The variable element set to consider.</param>
    /// <param name="alog">The log to consider.</param>
    /// <returns>Returns the source object.</returns>
    public delegate List<object> BdoHandlerPostFunction(
            object targetObject,
            ref IBdoMetaData sourceBdoElement,
            IBdoScope scope = null,
            IBdoMetaList varSet = null,
            IBdoLog alog = null);
}
