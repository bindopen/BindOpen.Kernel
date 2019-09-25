using System;
using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Items;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition;
using BindOpen.Framework.Core.Extensions.Libraries;
using BindOpen.Framework.Core.Extensions.Libraries.Definition;

namespace BindOpen.Framework.Core.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppExtension : IDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        AppDomain AppDomain { get; }

        /// <summary>
        /// 
        /// </summary>
        List<IScriptwordDefinition> ScriptwordDefinitions { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="library"></param>
        void AddLibrary(ILibrary library);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="libraries"></param>
        void AddLibraries(ILibrary[] libraries);

        /// <summary>
        /// 
        /// </summary>
        void Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="libraryNames"></param>
        /// <returns></returns>
        List<T> GetItemDefinitions<T>(string[] libraryNames = null) where T : IAppExtensionItemDefinition;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="libraryNames"></param>
        /// <returns></returns>
        List<string> GetItemDefinitionUniqueIds<T>(string[] libraryNames = null) where T : IAppExtensionItemDefinition;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uniqueId"></param>
        /// <param name="libraryNames"></param>
        /// <returns></returns>
        T GetItemDefinitionWithUniqueId<T>(string uniqueId, string[] libraryNames = null) where T : IAppExtensionItemDefinition;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        List<ILibrary> GetLibraries(string[] names = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ILibrary GetLibrary(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ILibraryDefinition GetLibraryDefinition(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        List<ILibraryDefinition> GetLibraryDefinitions(string[] names = null);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<string> GetLibraryNames();

        /// <summary>
        /// 
        /// </summary>
        void Initialize();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="definitionName"></param>
        /// <param name="libraryNames"></param>
        /// <returns></returns>
        List<IScriptwordDefinition> GetParentScriptwordDefinitions(
            string definitionName,
            string[] libraryNames = null);
    }
}