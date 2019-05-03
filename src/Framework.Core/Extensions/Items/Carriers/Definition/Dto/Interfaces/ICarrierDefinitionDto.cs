using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Source;

namespace BindOpen.Framework.Core.Extensions.Items.Carriers.Definition.Dto
{
    public interface ICarrierDefinitionDto : IAppExtensionItemDefinitionDto
    {
        DataSourceKind DataSourceKind { get; set; }
        string ItemClass { get; set; }
        DataElementSpecSet DetailSpec { get; set; }
    }
}