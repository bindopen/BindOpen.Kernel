using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Standard.Extensions.Carriers;
using System;
using System.Collections.Generic;

namespace BindOpen.Framework.Standard.Extensions.Handlers
{
    /// <summary>
    /// This static class represents the handler 
    /// </summary>
    public static class TextFromFile
    {
        /// <summary>
        /// Gets the target objects from the specified source.
        /// </summary>
        /// <param name="sourceElement">The source element to consider.</param>
        /// <param name="pathDetail">The path detail to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the target objects.</returns>
        public static List<Object> Get(
            DataElement sourceElement = null,
            DataElementSet pathDetail = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null)
        {
            List<Object> objects = new List<Object>();

            log = (log ?? new Log());

            if (sourceElement == null)
                log.AddError("Source element missing");
            else
            {
                RepositoryFile file = sourceElement.FirstItem as RepositoryFile;
                if (file == null)
                    log.AddError("Source file missing");
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
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the source object.</returns>
        public static List<Object> Post(
            Object targetObject,
            ref DataElement sourceDataElement,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null,
            Log log = null)
        {
            List<Object> objects = new List<Object>();

            return objects;
        }

    }
}
