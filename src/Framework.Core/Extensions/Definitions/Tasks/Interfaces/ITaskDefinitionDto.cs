using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Extensions.Items.Tasks;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Definitions.Tasks
{
    public interface ITaskDefinitionDto : IAppExtensionItemDefinitionDto
    {
        string ItemClass { get; set; }
        string GroupName { get; set; }

        DataElementSpecSet InputSpecification { get; set; }
        DataElementSpecSet OutputSpecification { get; set; }

        float MaximumIndex { get; set; }
        bool IsExecutable { get; set; }

        ILog CustomCheck();

        List<IDataElementSpec> GetEntries(params TaskEntryKind[] taskEntryKinds);
        object GetEntryDefaultItemWithName(string name, ILog log = null, params TaskEntryKind[] taskEntryKinds);
        IDataElementSpec GetEntryWithName(string key, params TaskEntryKind[] taskEntryKinds);

        void Repair(ITaskDefinitionDto taskDefinition);
    }
}