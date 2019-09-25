using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Extensions.Items.Tasks;
using BindOpen.Framework.Core.Extensions.Items.Tasks.Definition;

namespace BindOpen.Framework.Core.Extensions.Items.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITaskConfiguration
        : ITAppExtensionTitledItemConfiguration<ITaskDefinition>
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