using System;
using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Data.References
{
    /// <summary>
    /// This class represents a data binding handler.
    /// </summary>
    public static class DataBindingHandler
    {
        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Gets the items from the source of this instance and update the target items.
        /// </summary>
        /// <param name="aSourceElement">The source element to consider.</param>
        /// <param name="parameterDetail">The parameter detail to consider.</param>        
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the retrieved items.</returns>
        public static List<object> Get(
            DataElement aSourceElement,
            DataElementSet parameterDetail = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            log= (log ?? new Log());

            //this.SetDefinition((appScope== null ? null : appScope.AppExtension));
            //log.AddEvents(this.Check());

            List<object> dataItems = new List<object>();
            //parameterDetail = (parameterDetail ?? new DataElementSet());

            //if (!log.HasErrorsOrExceptions())
            //    if (this.Definition == null)
            //        log.AddError(title: "Definition not found");
            //    else if (this.Definition.RuntimeFunction_Get == null)
            //        log.AddError(title: "Calling function missing");
            //    else if (aSourceElement == null)
            //        log.AddError(title: "Source element missing");
            //    else
            //        dataItems.AddRange(this.Definition.RuntimeFunction_Get(aSourceElement, parameterDetail, appScope, scriptVariableSet, log));

            return dataItems;
        }

        /// <summary>
        /// Posts the items to the source of this instance.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        /// <param name="aSourceElement">The source element to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the posted items.</returns>
        public static List<object> Post(
            List<object> items,
            ref DataElement aSourceElement,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            //this.SetDefinition((appScope == null ? null : appScope.AppExtension));
            //log.AddEvents(this.Check());

            List<object> dataItems = new List<object>();

            //if (!log.HasErrorsOrExceptions())
            //    if (this.Definition == null)
            //        log.AddError(title: "Definition not found");
            //    else if (this.Definition.RuntimeFunction_Get == null)
            //        log.AddError(title: "Calling function missing");
            //    else if (aSourceElement == null)
            //        log.AddError(title: "Source element missing");
            //    else if ((items == null) || (items.Count == 0))
            //        log.AddWarning(title: "Target items missing");
            //    else
            //        foreach (object object1 in items)
            //            dataItems.AddRange(this.Definition.RuntimeFunction_Post(object1, ref aSourceElement, appScope, scriptVariableSet, log));

            return dataItems;
        }
        
        /// <summary>
        /// Posts the items to the source of this instance.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="aSourceElement">The source element to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>Returns the posted items.</returns>
        public static List<object> Post(
            Object aItem,
            DataElement aSourceElement,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            return DataBindingHandler.Post(new List<object>() { aItem }, aSourceElement, appScope, scriptVariableSet, log);
        }

        #endregion
    }
}