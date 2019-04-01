using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Extensions.Configuration.Tasks;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Definition.Tasks
{
    public interface ITaskDefinition : IAppExtensionItemDefinition
    {
        string GroupName { get; set; }
        IDataElementSpecSet InputSpecification { get; set; }
        bool IsExecutable { get; set; }
        string ItemClass { get; set; }
        IDataElementSpecSet OutputSpecification { get; set; }

        ILog CustomCheck();
        List<IDataElementSpec> GetEntries(params TaskEntryKind[] taskEntryKinds);
        object GetEntryDefaultItemWithName(string name, IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null, params TaskEntryKind[] taskEntryKinds);
        IDataElementSpec GetEntryWithName(string key, params TaskEntryKind[] taskEntryKinds);
        void Repair(ITaskDefinition taskDefinition);
    }
}