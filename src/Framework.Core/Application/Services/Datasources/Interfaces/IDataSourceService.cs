using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Items.Connectors;

namespace BindOpen.Framework.Core.Application.Services.Data.Datasources
{
    public interface IDataSourceService : IDataItem
    {
        IDataItemSet<DataSource> DataSourceSet { get; }

        void AddSource(IDataSource source);
        void AddSource(params IDataSource[] sources);
        void Clear();
        IConnectorConfiguration GetConnectorConfiguration(string sourceName, string connectorDefinitionUniqueId);
        string GetInstanceName(string sourceName);
        string GetInstanceOtherwiseModuleName(string sourceName);
        string GetModuleName(string sourceName);
        IDataSource GetSource(string sourceName);
        string GetStringConnection(string sourceName, string connectorDefinitionUniqueId);
        bool HasConnectorConfiguration(string sourceName, string connectorDefinitionUniqueId);
        bool HasSource(string sourceName);
        void RemoveSource(params string[] sourceNames);
    }
}