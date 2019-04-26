using BindOpen.Framework.Core.Application.Scopes.Interfaces;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Extensions.Configuration.Tasks.Interfaces;
using BindOpen.Framework.Core.System.Diagnostics.Interfaces;
using BindOpen.Framework.Core.System.Scripting.Interfaces;

namespace BindOpen.Framework.Core.Application.Commands.Interfaces
{
    public interface ICommand : ITaskConfiguration
    {
        CommandKind Kind { get; set; }

        ILog ExecuteWithResult(
            out string resultString,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            RuntimeMode runtimeMode = RuntimeMode.Normal);
    }
}