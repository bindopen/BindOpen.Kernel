using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Items.Connectors;
using BindOpen.Framework.Core.Extensions.Items.Connectors;

namespace BindOpen.Framework.Core.Data.Items.Source
{
    public interface IDataSource : INamedDataItem
    {
        List<ConnectorDto> Configurations { get; set; }

        string InstanceName { get; set; }

        DataSourceKind Kind { get; set; }

        string ModuleName { get; set; }

        IConnectorDto GetConfiguration(string definitionName);
        bool HasConfiguration(string definitionName);

        void AddConfiguration(IConnectorDto config);

        void RemoveConfiguration(string definitionName);
    }
}