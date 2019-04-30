using System;
using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Items;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition;
using BindOpen.Framework.Core.Extensions.Libraries;
using BindOpen.Framework.Core.Extensions.Libraries.Definition;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions
{
    public interface IAppExtension : IDataItem
    {
        AppDomain AppDomain { get; }

        List<IScriptwordDefinition> ScriptwordDefinitions { get; }

        void AddLibrary(ILibrary library);
        void AddLibraries(ILibrary[] libraries);
        void Clear();
        List<T> GetItemDefinitions<T>(string[] libraryNames = null) where T : IAppExtensionItemDefinition;
        List<string> GetItemDefinitionUniqueIds<T>(string[] libraryNames = null) where T : IAppExtensionItemDefinition;
        T GetItemDefinitionWithUniqueId<T>(string uniqueId, string[] libraryNames = null) where T : IAppExtensionItemDefinition;
        List<ILibrary> GetLibraries(string[] names = null);
        ILibrary GetLibrary(string name);
        ILibraryDefinition GetLibraryDefinition(string name);
        List<ILibraryDefinition> GetLibraryDefinitions(string[] names = null);
        List<string> GetLibraryNames();

        void Initialize();

        ILog AddLibraries(
            IAppExtensionConfiguration config,
            string folderPath = null);
        ILog AddLibrariesFromFile(
            string filePaths,
            string folderPath);

        List<IScriptwordDefinition> GetParentScriptwordDefinitions(
            string definitionName,
            string[] libraryNames = null);
    }
}