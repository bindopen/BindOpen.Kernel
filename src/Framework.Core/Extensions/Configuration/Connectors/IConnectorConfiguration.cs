using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Extensions.Definition.Connectors;

namespace BindOpen.Framework.Core.Extensions.Configuration.Connectors
{
    public interface IConnectorConfiguration : ITAppExtensionTitledItemConfiguration<IConnectorDefinition>
    {
        string ConnectionString { get; set; }
        DataSourceKind DataSourceKind { get; }
        IDataElementSet Detail { get; set; }

        IDataElementSet NewDetail();
        void UpdateConnectionString(string connectionString = null);
    }
}