using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Items.Connectors;

namespace BindOpen.Framework.Core.Application.Depots.Datasources
{
    public interface IDataSourceDepot : IDataItemSet<DataSource>
    {
        List<DataSource> Sources {get; set; }
        IConnectorConfiguration GetConnectorConfiguration(string sourceName, string connectorDefinitionUniqueId);
        string GetInstanceName(string sourceName);
        string GetInstanceOtherwiseModuleName(string sourceName);
        string GetModuleName(string sourceName);
        string GetStringConnection(string sourceName, string connectorDefinitionUniqueId);
        bool HasConnectorConfiguration(string sourceName, string connectorDefinitionUniqueId);
    }
}