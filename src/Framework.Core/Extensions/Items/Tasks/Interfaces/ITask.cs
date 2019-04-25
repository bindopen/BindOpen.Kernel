using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Extensions.Definitions.Tasks;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Items.Tasks
{
    public interface ITask : ITAppExtensionItem<ITaskDefinition>
    {
        object GetEntryObjectWithName(
            string name,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null,
            params TaskEntryKind[] taskEntryKinds);

        bool IsCompatibleWith(IDataElementSpecSet dataElementSpecSet, TaskEntryKind taskEntryKind = TaskEntryKind.Any);
        bool IsConfigurable(SpecificationLevel specificationLevel = SpecificationLevel.Runtime);

        void UpdateAbsolutePaths(string relativePath);

        void Execute(
            ILog log,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            RuntimeMode runtimeMode = RuntimeMode.Normal);
    }
}