using BindOpen.Framework.Core.Application.Datasources;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Context;
using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Runtime.Application.Services
{
    public interface IScopedService
    {
        AppExtension AppExtension { get; }
        IAppScope AppScope { get; }
        IConnectionService ConnectionService { get; }
        DataContext DataContext { get; }
        DataSourceService DataSourceService { get; }
        ScriptInterpreter ScriptInterpreter { get; }
    }
}