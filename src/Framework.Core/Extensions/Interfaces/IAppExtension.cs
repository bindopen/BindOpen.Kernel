using System;
using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Definition;
using BindOpen.Framework.Core.Extensions.Definition.Libraries;
using BindOpen.Framework.Core.Extensions.Items.Libraries;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions
{
    public interface IAppExtension : IDataItem
    {
        AppDomain AppDomain { get; }

        void AddLibrary(ILibrary library);
        void AddLibraries(ILibrary[] libraries);
        void Clear();
        List<T> GetItemDefinitions<T>(string[] libraryNames = null) where T : IAppExtensionItemDefinition;
        List<string> GetItemDefinitionUniqueIds<T>(string[] libraryNames = null) where T : IAppExtensionItemDefinition;
        T GetItemDefinitionWithUniqueId<T>(string uniqueId, string[] libraryNames = null) where T : IAppExtensionItemDefinition;
        List<ILibrary> GetLibraries(string[] names = null);
        ILibrary GetLibrary(string name);
        ILibraryDefinitionDto GetLibraryDefinition(string name);
        List<ILibraryDefinitionDto> GetLibraryDefinitions(string[] names = null);
        List<string> GetLibraryNames();

        void Initialize();

        ILog AddLibraries(
            IAppExtensionConfiguration config,
            string folderPath = null);
        ILog AddLibrariesFromFile(
            string filePaths,
            string folderPath);
    }
}