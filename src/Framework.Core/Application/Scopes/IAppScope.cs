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
    public interface IAppScope
    {
        AppExtension AppExtension { get; set; }
        DataContext DataContext { get; set; }
        DataSourceService DataSourceService { get; set; }
        ScriptInterpreter ScriptInterpreter { get; set; }

        ITAppExtensionRuntimeItem<T> CreateItem<T>(string definitionName, TAppExtensionItemConfiguration<T> configuration, string name = null, Log log = null) where T : AppExtensionItemDefinition;
        object CreateViewer(AppExtensionItemKind libraryItemKind, string definitionName, string viewerKey, Log log = null);
        void SetAppDomain(AppDomain appDomain);
        Log Update<T>(T item = null, List<string> specificationAreas = null, List<UpdateMode> updateModes = null, IAppScope appScope = null, ScriptVariableSet scriptVariableSet = null) where T : DataItem;
    }
}