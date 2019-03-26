using BindOpen.Framework.Core.Application.Datasources;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Context;
using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// This interface defines a scoped service.
    /// </summary>
    public interface IScopedService
    {
        /// <summary>
        /// The application extension.
        /// </summary>
        AppExtension AppExtension { get; }

        /// <summary>
        /// The application scope.
        /// </summary>
        IAppScope AppScope { get; }

        /// <summary>
        /// The connection service.
        /// </summary>
        IConnectionService ConnectionService { get; }

        /// <summary>
        /// The data context.
        /// </summary>
        DataContext DataContext { get; }

        /// <summary>
        /// The data source service.
        /// </summary>
        DataSourceService DataSourceService { get; }

        /// <summary>
        /// The script interpreter.
        /// </summary>
        ScriptInterpreter ScriptInterpreter { get; }
    }
}