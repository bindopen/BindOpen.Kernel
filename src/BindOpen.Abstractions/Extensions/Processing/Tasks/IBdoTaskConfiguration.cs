using BindOpen.Data.Meta;
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
        IBdoMetaSet InputDetail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        IBdoTaskConfiguration AddInputs(params IBdoMetaData[] inputs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        IBdoTaskConfiguration WithInputs(params IBdoMetaData[] inputs);

        /// <summary>
        /// The output detail.
        /// </summary>
        IBdoMetaSet OutputDetail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        IBdoTaskConfiguration AddOutputs(params IBdoMetaData[] outputs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        IBdoTaskConfiguration WithOutputs(params IBdoMetaData[] outputs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskEntryKinds"></param>
        /// <returns></returns>
        List<IBdoMetaData> GetEntries(params TaskEntryKind[] taskEntryKinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="taskEntryKinds"></param>
        /// <returns></returns>
        IBdoMetaData GetEntryWithName(string key, params TaskEntryKind[] taskEntryKinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoTaskConfiguration Add(params IBdoMetaData[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoTaskConfiguration WithItems(params IBdoMetaData[] items);
    }
}