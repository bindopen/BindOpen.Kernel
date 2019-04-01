using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Configuration.Connectors;

namespace BindOpen.Framework.Core.Application.Datasources
{
    public interface IDataSourceService : IDataItem
    {
        DataItemSet<IDataSource> DataSourceSet { get; }

        void AddSource(IDataSource source);
        void AddSource(params IDataSource[] sources);
        void Clear();
        ConnectorConfiguration GetConnectorConfiguration(string sourceName, string connectorDefinitionUniqueName);
        string GetInstanceName(string sourceName);
        string GetInstanceOtherwiseModuleName(string sourceName);
        string GetModuleName(string sourceName);
        IDataSource GetSource(string sourceName);
        string GetStringConnection(string sourceName, string connectorDefinitionUniqueName);
        bool HasConnectorConfiguration(string sourceName, string connectorDefinitionUniqueName);
        bool HasSource(string sourceName);
        void RemoveSource(params string[] sourceNames);
    }
}