using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Definition;

namespace BindOpen.Framework.Core.Extensions.Indexes
{
    public interface ITAppExtensionItemIndex<T> : IStoredDataItem where T : IAppExtensionItemDefinition
    {
        List<T> Definitions { get; set; }
        List<IAppExtensionItemGroup> Groups { get; }
        string LibraryId { get; set; }
        string LibraryName { get; set; }
    }
}