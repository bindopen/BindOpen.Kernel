using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Extensions.Definition
{
    public interface IAppExtensionItemGroup : IDescribedDataItem
    {
        global::System.Collections.Generic.List<IAppExtensionItemGroup> SubGroups { get; }

        IAppExtensionItemGroup GetGroupWithName(string name);
    }
}