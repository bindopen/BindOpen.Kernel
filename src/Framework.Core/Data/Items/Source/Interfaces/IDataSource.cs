using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Items.Connectors;
using BindOpen.Framework.Core.Extensions.Items.Connectors;

namespace BindOpen.Framework.Core.Data.Items.Source
{
    public interface IDataSource : INamedDataItem
    {
        List<ConnectorConfiguration> Configurations { get; set; }

        string InstanceName { get; set; }

        DataSourceKind Kind { get; set; }

        string ModuleName { get; set; }

        IConnectorConfiguration GetConfiguration(string definitionName);
        bool HasConfiguration(string definitionName);

        void AddConfiguration(IConnectorConfiguration config);

        void RemoveConfiguration(string definitionName);
    }
}