using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Source;

namespace BindOpen.Framework.Core.Extensions.Definition.Carriers
{
    public interface ICarrierDefinitionDto : IAppExtensionItemDefinitionDto
    {
        DataSourceKind DataSourceKind { get; set; }
        string ItemClass { get; set; }
        DataElementSpecSet DetailSpec { get; set; }
    }
}