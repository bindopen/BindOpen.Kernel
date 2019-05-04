using BindOpen.Framework.Core.Application.Depots.Datasources;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Context;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Runtime.Application.Services
{
    /// <summary>
    /// This interface defines a scoped service.
    /// </summary>
    public interface IScopedService
    {
        /// <summary>
        /// The application scope.
        /// </summary>
        IAppScope Scope { get; }

        /// <summary>
        /// The connection service.
        /// </summary>
        IConnectionService ConnectionService { get; }

        /// <summary>
        /// The data context.
        /// </summary>
        IDataContext Context { get; }

        /// <summary>
        /// The data source service.
        /// </summary>
        IDataSourceDepot DataSourceDepot { get; }

        /// <summary>
        /// The script interpreter.
        /// </summary>
        IScriptInterpreter Interpreter { get; }
    }
}