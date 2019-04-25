﻿using System.Collections.Generic;
using System.Reflection;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Definitions;
using BindOpen.Framework.Core.Extensions.Definitions.Libraries;
using BindOpen.Framework.Core.Extensions.Definitions.Scriptwords;

namespace BindOpen.Framework.Core.Extensions.Libraries
{
    public interface ILibrary : INamedDataItem
    {
        Assembly Assembly { get; set; }
        ILibraryDefinitionDto Definition { get; set; }
        string DefinitionName { get; }
        DataSourceKind SourceKind { get; set; }

        void Add<T>(T definition) where T : IAppExtensionItemDefinition;
        void Clear();
        void Delete<T>(string key) where T : IAppExtensionItemDefinition;
        List<T> GetItemDefinitions<T>() where T : IAppExtensionItemDefinition;
        T GetItemDefinitionWithUniqueId<T>(string uniqueName) where T : IAppExtensionItemDefinition;
        string GetNormalizedName();
        IScriptwordDefinition GetScriptwordDefinitionWithUniqueName(string uniqueName, IScriptwordDefinition parentDefinition = null);
        void Initialize(IAppExtension appExtension);
    }
}