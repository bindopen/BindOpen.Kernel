using BindOpen.Application.Scopes;
using BindOpen.Data.Elements;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using BindOpen.Extensions.Carriers;
using System;
using System.Collections.Generic;

namespace BindOpen.Extensions.Handlers
{
    /// <summary>
    /// This static class represents the handler 
    /// </summary>
    public static class ObjectFromFile
    {
        /// <summary>
        /// Gets the target objects from the specified source.
        /// </summary>
        /// <param name="sourceElement">The source element to consider.</param>
        /// <param name="pathDetail">The path detail to consider.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the target objects.</returns>
        public static List<object> Get(
            DataElement sourceElement = null,
            DataElementSet pathDetail = null,
            IBdoScope scope = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            List<object> objects = new List<object>();

            if (sourceElement == null)
                log?.AddError("Source element missing");
            else
            {
                RepositoryFile file = sourceElement.Items[0] as RepositoryFile;
                if (file == null)
                    log?.AddError("Source file missing");
                else
                {

                }
            }

            return objects;
        }

        /// <summary>
        /// Posts the selected targets to the source.
        /// </summary>
        /// <param name="targetObject">The target object to consider.</param>
        /// <param name="sourceDataElement">The source data element to consider.</param>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the source object.</returns>
        public static List<object> Post(
            Object targetObject,
            ref DataElement sourceDataElement,
            IBdoScope scope = null,
            IBdoScriptVariableSet scriptVariableSet = null,
            IBdoLog log = null)
        {
            List<object> objects = new List<object>();

            return objects;
        }

    }
}
