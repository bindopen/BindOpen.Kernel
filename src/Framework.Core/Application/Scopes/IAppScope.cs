using System;
using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Datasources;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Context;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.Core.Extensions.Common;
using BindOpen.Framework.Core.Extensions.Configuration;
using BindOpen.Framework.Core.Extensions.Definition;
using BindOpen.Framework.Core.Extensions.Runtime;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Application.Scopes
{
    /// <summary>
    /// This interface defines an application scope.
    /// </summary>
    public interface IAppScope
    {
        /// <summary>
        /// The application extension.
        /// </summary>
        AppExtension AppExtension { get; set; }

        /// <summary>
        /// The data context.
        /// </summary>
        DataContext DataContext { get; set; }

        /// <summary>
        /// The data source service.
        /// </summary>
        DataSourceService DataSourceService { get; set; }

        /// <summary>
        /// The script interpreter.
        /// </summary>
        ScriptInterpreter ScriptInterpreter { get; set; }

        /// <summary>
        /// Creates a new instance of extension item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="definitionName"></param>
        /// <param name="configuration"></param>
        /// <param name="name"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        ITAppExtensionRuntimeItem<T> CreateItem<T>(string definitionName, TAppExtensionItemConfiguration<T> configuration, string name = null, Log log = null) where T : AppExtensionItemDefinition;

        /// <summary>
        /// Creates a new instance of viewer.
        /// </summary>
        /// <param name="libraryItemKind"></param>
        /// <param name="definitionName"></param>
        /// <param name="viewerKey"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        object CreateViewer(AppExtensionItemKind libraryItemKind, string definitionName, string viewerKey, Log log = null);

        /// <summary>
        /// Sets the application domain of this instance.
        /// </summary>
        /// <param name="appDomain">The application domain to consider.</param>
        void SetAppDomain(AppDomain appDomain);

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="specificationAreas"></param>
        /// <param name="updateModes"></param>
        /// <param name="appScope"></param>
        /// <param name="scriptVariableSet"></param>
        /// <returns></returns>
        Log Update<T>(T item = null, List<string> specificationAreas = null, List<UpdateMode> updateModes = null, IAppScope appScope = null, ScriptVariableSet scriptVariableSet = null) where T : DataItem;
    }
}