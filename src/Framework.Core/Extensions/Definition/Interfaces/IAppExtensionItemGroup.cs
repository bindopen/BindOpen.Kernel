using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Extensions.Definition
{
    public interface IAppExtensionItemGroup : IDescribedDataItem
    {
        List<AppExtensionItemGroup> SubGroups { get; }

        IAppExtensionItemGroup GetGroupWithName(string name);
    }
}