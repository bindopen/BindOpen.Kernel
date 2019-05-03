using BindOpen.Framework.Core.Data.Items.Source;

namespace BindOpen.Framework.Core.Extensions.Items.Formats.Definition.Dto
{
    public interface IFormatDefinitionDto : IAppExtensionItemDefinitionDto
    {
        DataSourceKind DataSourceKind { get; set; }
        string ItemClass { get; set; }
        string ViewerClass { get; set; }
    }
}