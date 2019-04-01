using System;
using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Definition;
using BindOpen.Framework.Core.Extensions.Definition.Libraries;
using BindOpen.Framework.Core.Extensions.Definition.Scriptwords;
using BindOpen.Framework.Core.Extensions.Runtime.Libraries;

namespace BindOpen.Framework.Core.Extensions
{
    public interface IAppExtension : IDataItem
    {
        AppDomain AppDomain { get; }

        void AddLibrary(ILibrary library);
        void Clear();
        List<T> GetItemDefinitions<T>(string[] libraryNames = null) where T : IAppExtensionItemDefinition;
        List<string> GetItemDefinitionUniqueIds<T>(string[] libraryNames = null) where T : IAppExtensionItemDefinition;
        T GetItemDefinitionWithUniqueId<T>(string uniqueId, string[] libraryNames = null) where T : IAppExtensionItemDefinition;
        List<ILibrary> GetLibraries(string[] names = null);
        ILibrary GetLibrary(string name);
        ILibraryDefinition GetLibraryDefinition(string name);
        List<ILibraryDefinition> GetLibraryDefinitions(string[] names = null);
        List<string> GetLibraryNames();
        List<IScriptWordDefinition> GetParentScriptWordDefinitions(string definitionName, string[] libraryNames = null);
        void Initialize();
    }
}