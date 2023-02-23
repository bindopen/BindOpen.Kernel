using BindOpen.Data.Meta;
using BindOpen.Extensions;
using BindOpen.Logging;
using System;
using System.Collections.Generic;

namespace BindOpen.Runtime.Definitions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoTaskDefinition : IBdoExtensionDefinition
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
        IBdoSpecSet InputSpecification { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoSpecSet OutputSpecification { get; set; }

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
        /// <param key="taskEntryKinds"></param>
        /// <returns></returns>
        List<IBdoSpec> GetEntries(
            params TaskEntryKind[] taskEntryKinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <param key="log"></param>
        /// <param key="taskEntryKinds"></param>
        /// <returns></returns>
        object GetEntryDefaultItemWithName(string name, IBdoLog log = null, params TaskEntryKind[] taskEntryKinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="key"></param>
        /// <param key="taskEntryKinds"></param>
        /// <returns></returns>
        IBdoSpec GetEntryWithName(string key, params TaskEntryKind[] taskEntryKinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="taskDefinition"></param>
        void Repair(IBdoTaskDefinition taskDefinition);
    }
}