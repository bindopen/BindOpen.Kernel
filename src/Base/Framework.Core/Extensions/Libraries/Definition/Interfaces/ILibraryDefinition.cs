using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dictionary;

namespace BindOpen.Framework.Core.Extensions.Libraries.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILibraryDefinition : IDescribedDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        string AssemblyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string FileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string GroupName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DictionaryDataItem ItemIndexFullNameDictionary { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string RootNamespace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<string> UsingAssemblyFileNames { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetRootNamespace();

        /// <summary>
        /// 
        /// </summary>
        void Initialize();
    }
}