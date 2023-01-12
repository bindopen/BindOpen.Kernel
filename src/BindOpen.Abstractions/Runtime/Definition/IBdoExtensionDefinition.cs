using BindOpen.Meta;
using BindOpen.Meta.Items;
using System.Collections.Generic;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// This interface defines the extension definition.
    /// </summary>
    public interface IBdoExtensionDefinition : IReferenced,
        ITIdentifiedPoco<IBdoExtensionDefinition>,
        ITNamedPoco<IBdoExtensionDefinition>,
        ITGloballyTitledPoco<IBdoExtensionDefinition>,
        ITGloballyDescribedPoco<IBdoExtensionDefinition>
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
        /// <returns></returns>
        string GetRootNamespace();

        /// <summary>
        /// 
        /// </summary>
        string GroupName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        void Initialize();

        /// <summary>
        /// 
        /// </summary>
        IBdoDictionary ItemIndexFullNameDictionary { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string RootNamespace { get; set; }

        /// <summary>
        /// The unique ID of this instance.
        /// </summary> 
        string UniqueId { get; }

        /// <summary>
        /// 
        /// </summary>
        List<string> UsingAssemblyFileNames { get; set; }
    }
}