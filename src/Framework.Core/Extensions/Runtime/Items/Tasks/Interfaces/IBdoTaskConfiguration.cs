using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Extensions.Definition.Items;
using System.Collections.Generic;

namespace BindOpen.Framework.Core.Extensions.Runtime.Items
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoTaskConfiguration
        : ITBdoExtensionTitledItemConfiguration<IBdoTaskDefinition>
    {
        /// <summary>
        /// The input detail.
        /// </summary>
        DataElementSet InputDetail { get; set; }

        /// <summary>
        /// The output detail.
        /// </summary>
        DataElementSet OutputDetail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskEntryKinds"></param>
        /// <returns></returns>
        List<IDataElement> GetEntries(params TaskEntryKind[] taskEntryKinds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="taskEntryKinds"></param>
        /// <returns></returns>
        IDataElement GetEntryWithName(string key, params TaskEntryKind[] taskEntryKinds);
    }
}