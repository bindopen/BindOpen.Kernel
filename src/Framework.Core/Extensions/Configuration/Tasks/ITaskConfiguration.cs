using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Extensions.Definition.Tasks;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Configuration.Tasks
{
    public interface ITaskConfiguration : ITAppExtensionTitledItemConfiguration<ITaskDefinition>
    {
        IDataElementSet Detail { get; set; }
        IDataElementSet InputDetail { get; set; }
        float MaximumIndex { get; set; }
        IDataElementSet OutputDetail { get; set; }
        bool OutputDetailSpecified { get; }

        List<IDataElement> GetEntries(params TaskEntryKind[] taskEntryKinds);
        object GetEntryItemObjectWithName(string name, IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, ILog log = null, params TaskEntryKind[] taskEntryKinds);
        IDataElement GetEntryWithName(string key, params TaskEntryKind[] taskEntryKinds);
        bool IsCompatibleWith(IDataElementSpecSet dataElementSpecSet, TaskEntryKind taskEntryKind = TaskEntryKind.Any);
        bool IsConfigurable(SpecificationLevel specificationLevel = SpecificationLevel.Runtime);
        void UpdateAbsolutePaths(string relativePath);
    }
}