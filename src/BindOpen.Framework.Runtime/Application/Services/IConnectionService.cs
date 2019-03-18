using System.Collections.Generic;
using BindOpen.Framework.Core.Application.Datasources;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Connections;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Configuration.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Runtime.Application.Services
{
    public interface IConnectionService
    {
        //List<Connection> Connections { get; set; }

        Log Close(Connection connector);
        //Connection GetConnection(string name);
        //bool IsOpened(string name);
        T Open<T>(ConnectorConfiguration configuration, Log log = null) where T : Connection, new();
        T Open<T>(DataSource dataSource, string connectorDefinitionUniqueName, Log log = null) where T : Connection, new();
        T Open<T>(DataSourceService dataSourceManager, string dataSourceName, string connectorDefinitionUniqueName, Log log = null) where T : Connection, new();
        T Open<T>(string dataSourceName, string connectorDefinitionUniqueName, Log log = null) where T : Connection, new();
        Log Update<T>(T item = null, List<string> specificationAreas = null, List<UpdateMode> updateModes = null, IAppScope appScope = null, ScriptVariableSet scriptVariableSet = null) where T : DataItem;
    }
}