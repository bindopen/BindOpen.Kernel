using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Extensions.Items.Tasks;
using BindOpen.Framework.Core.Extensions.Items.Tasks.Definition;

namespace BindOpen.Framework.Core.Extensions.Items.Tasks
{
    public interface ITaskConfiguration
        : ITAppExtensionTitledItemConfiguration<ITaskDefinition>
    {
        DataElementSet InputDetail { get; set; }

        DataElementSet OutputDetail { get; set; }

        List<IDataElement> GetEntries(params TaskEntryKind[] taskEntryKinds);

        IDataElement GetEntryWithName(string key, params TaskEntryKind[] taskEntryKinds);
    }
}