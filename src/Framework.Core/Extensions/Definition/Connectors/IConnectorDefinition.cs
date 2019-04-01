using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Source;

namespace BindOpen.Framework.Core.Extensions.Definition.Connectors
{
    public interface IConnectorDefinition : IAppExtensionItemDefinition
    {
        DataSourceKind DataSourceKind { get; set; }
        IDataElementSpecSet DetailSpec { get; set; }
        string ItemClass { get; set; }
    }
}