using System.Collections.Generic;
using BindOpen.Framework.Core.Extensions.Definition;

namespace BindOpen.Framework.Core.Extensions.Indexes
{
    public interface ITAppExtensionItemIndexDto<T> : IAppExtensionItemIndexDto where T : IAppExtensionItemDefinitionDto
    {
        List<T> Definitions { get; set; }
        List<IAppExtensionItemGroup> Groups { get; }
        string LibraryId { get; set; }
        string LibraryName { get; set; }
    }
}