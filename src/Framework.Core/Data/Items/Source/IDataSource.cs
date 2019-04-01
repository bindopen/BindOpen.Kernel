using System.Collections.Generic;
using BindOpen.Framework.Core.Extensions.Configuration.Connectors;

namespace BindOpen.Framework.Core.Data.Items.Source
{
    public interface IDataSource : INamedDataItem
    {
        List<ConnectorConfiguration> Configurations { get; set; }

        string InstanceName { get; set; }

        DataSourceKind Kind { get; set; }

        string ModuleName { get; set; }

        ConnectorConfiguration GetConfiguration(string definitionName);
        bool HasConfiguration(string definitionName);

        void AddConfiguration(ConnectorConfiguration connector);

        void RemoveConfiguration(string definitionName);
    }
}