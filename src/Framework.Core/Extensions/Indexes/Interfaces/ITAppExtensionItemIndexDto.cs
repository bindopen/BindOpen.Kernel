using System.Collections.Generic;
using BindOpen.Framework.Core.Extensions.Definitions;

namespace BindOpen.Framework.Core.Extensions.Indexes
{
    public interface ITAppExtensionItemIndexDto<T> : IAppExtensionItemIndexDto where T : AppExtensionItemDefinitionDto
    {
        List<T> Definitions { get; set; }
        List<AppExtensionItemGroup> Groups { get; }
        string LibraryId { get; set; }
        string LibraryName { get; set; }
    }
}