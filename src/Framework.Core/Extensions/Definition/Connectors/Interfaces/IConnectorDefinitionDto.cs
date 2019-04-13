using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Source;

namespace BindOpen.Framework.Core.Extensions.Definition.Connectors
{
    public interface IConnectorDefinitionDto : IAppExtensionItemDefinitionDto
    {
        DataSourceKind DataSourceKind { get; set; }
        DataElementSpecSet DatasourceDetailSpec { get; set; }
        string ItemClass { get; set; }
    }
}