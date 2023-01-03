using BindOpen.Extensions;
using BindOpen.Data.Elements;
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
        IBdoTaskConfiguration AddInputs(params IBdoElement[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        IBdoTaskConfiguration WithInputs(params IBdoElement[] items);

        /// <summary>
        /// The output detail.
        /// </summary>
        IBdoElementSet OutputDetail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        IBdoTaskConfiguration AddOutputs(params IBdoElement[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        IBdoTaskConfiguration WithOutputs(params IBdoElement[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskEntryKinds"></param>
        /// <returns></returns>
        List<IBdoElement> GetEntries(params TaskEntryKind[] taskEntryKinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="taskEntryKinds"></param>
        /// <returns></returns>
        IBdoElement GetEntryWithName(string key, params TaskEntryKind[] taskEntryKinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoTaskConfiguration Add(params IBdoElement[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoTaskConfiguration WithItems(params IBdoElement[] items);
    }
}