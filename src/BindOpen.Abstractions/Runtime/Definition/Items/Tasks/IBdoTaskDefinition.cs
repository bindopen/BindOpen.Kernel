using BindOpen.Logging;
using BindOpen.Data.Meta;
using System;
using System.Collections.Generic;
using BindOpen.Data.Meta;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoTaskDefinition : IBdoExtensionItemDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        Type RuntimeType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ItemClass { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string GroupName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaSpecSet InputSpecification { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaSpecSet OutputSpecification { get; set; }

        /// <summary>
        /// 
        /// </summary>
        float MaximumIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsExecutable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskEntryKinds"></param>
        /// <returns></returns>
        List<IBdoMetaDataSpec> GetEntries(params TaskEntryKind[] taskEntryKinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="log"></param>
        /// <param name="taskEntryKinds"></param>
        /// <returns></returns>
        object GetEntryDefaultItemWithName(string name, IBdoLog log = null, params TaskEntryKind[] taskEntryKinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="taskEntryKinds"></param>
        /// <returns></returns>
        IBdoMetaDataSpec GetEntryWithName(string key, params TaskEntryKind[] taskEntryKinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskDefinition"></param>
        void Repair(IBdoTaskDefinition taskDefinition);
    }
}