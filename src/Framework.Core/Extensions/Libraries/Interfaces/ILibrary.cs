using System.Collections.Generic;
using System.Reflection;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Items;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition;
using BindOpen.Framework.Core.Extensions.Libraries.Definition;

namespace BindOpen.Framework.Core.Extensions.Libraries
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILibrary : INamedDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        Assembly Assembly { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ILibraryDefinition Definition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string DefinitionName { get; }

        /// <summary>
        /// 
        /// </summary>
        DataSourceKind SourceKind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="definition"></param>
        void Add<T>(T definition) where T : IAppExtensionItemDefinition;

        /// <summary>
        /// 
        /// </summary>
        void Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        void Delete<T>(string key) where T : IAppExtensionItemDefinition;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> GetItemDefinitions<T>() where T : IAppExtensionItemDefinition;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uniqueName"></param>
        /// <returns></returns>
        T GetItemDefinitionWithUniqueId<T>(string uniqueName) where T : IAppExtensionItemDefinition;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetNormalizedName();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uniqueName"></param>
        /// <param name="parentDefinition"></param>
        /// <returns></returns>
        IScriptwordDefinition GetScriptwordDefinitionWithUniqueName(string uniqueName, IScriptwordDefinition parentDefinition = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appExtension"></param>
        void Initialize(IAppExtension appExtension);
    }
}