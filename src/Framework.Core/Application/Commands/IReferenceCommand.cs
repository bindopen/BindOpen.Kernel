using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.References;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Application.Commands
{
    public interface IReferenceCommand : ICommand
    {
        IDataReference Reference { get; set; }

        ILog ExecuteWithResult(out string resultString, IAppScope appScope = null, IScriptVariableSet scriptVariableSet = null, RuntimeMode runtimeMode = RuntimeMode.Normal);
    }
}