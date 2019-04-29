using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Diagnostics.Loggers;

namespace BindOpen.Framework.Core.Extensions.Items
{
    public interface IAppExtensionItemDefinitionDto : IIndexedDataItem
    {
        string ImageUrl { get; set; }
        bool IsEditable { get; set; }
        bool IsIndexed { get; set; }
        string LibraryName { get; set; }

        string GetText(LogFormat logFormat = LogFormat.Xml, string uiCulture = "*");
    }
}