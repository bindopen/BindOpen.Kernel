using BindOpen.Framework.Core.Extensions.Definition.Connectors;

namespace BindOpen.Framework.Core.Extensions.Items.Connectors
{
    public interface IConnectorDto : ITAppExtensionTitledItemDto<IConnectorDefinition>
    {
        string ConnectionString { get; set; }
    }
}