﻿using BindOpen.Data;
using BindOpen.Data.Assemblies;
using System.Collections.Generic;

namespace BindOpen.Scoping
{
    /// <summary>
    /// This interface defines the extension definition.
    /// </summary>
    public interface IBdoPackageDefinition : IReferenced,
        IIdentified, INamed,
        IBdoTitled, IBdoDescribed
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
        string Alias { get; set; }

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
        ITBdoDictionary<string> ItemIndexFullNameDictionary { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string RootNamespace { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IList<IBdoAssemblyReference> UsingAssemblyReferences { get; set; }
    }
}