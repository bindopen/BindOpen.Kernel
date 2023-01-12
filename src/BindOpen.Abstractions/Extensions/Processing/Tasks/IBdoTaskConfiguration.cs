﻿using BindOpen.Meta.Elements;
using BindOpen.Runtime.Definition;
using System.Collections.Generic;

namespace BindOpen.Extensions.Processing
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoTaskConfiguration : ITBdoExtensionTitledItemConfiguration<IBdoTaskDefinition>
    {
        /// <summary>
        /// The input detail.
        /// </summary>
        IBdoElementSet InputDetail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        IBdoTaskConfiguration AddInputs(params IBdoMetaElement[] inputs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        IBdoTaskConfiguration WithInputs(params IBdoMetaElement[] inputs);

        /// <summary>
        /// The output detail.
        /// </summary>
        IBdoElementSet OutputDetail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        IBdoTaskConfiguration AddOutputs(params IBdoMetaElement[] outputs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        IBdoTaskConfiguration WithOutputs(params IBdoMetaElement[] outputs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskEntryKinds"></param>
        /// <returns></returns>
        List<IBdoMetaElement> GetEntries(params TaskEntryKind[] taskEntryKinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="taskEntryKinds"></param>
        /// <returns></returns>
        IBdoMetaElement GetEntryWithName(string key, params TaskEntryKind[] taskEntryKinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoTaskConfiguration Add(params IBdoMetaElement[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoTaskConfiguration WithItems(params IBdoMetaElement[] items);
    }
}