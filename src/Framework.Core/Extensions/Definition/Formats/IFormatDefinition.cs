using BindOpen.Framework.Core.Data.Items.Source;

namespace BindOpen.Framework.Core.Extensions.Definition.Formats
{
    public interface IFormatDefinition : IAppExtensionItemDefinition
    {
        DataSourceKind DataSourceKind { get; set; }
        string ItemClass { get; set; }
        string ViewerClass { get; set; }
    }
}