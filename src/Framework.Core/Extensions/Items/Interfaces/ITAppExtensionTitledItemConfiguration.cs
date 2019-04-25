using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dto;
using BindOpen.Framework.Core.Extensions.Definitions;

namespace BindOpen.Framework.Core.Extensions.Items
{
    public interface ITAppExtensionTitledItemConfiguration<T>
        : ITAppExtensionItemConfiguration<T>, IGloballyTitled, INamed, IIdentifiedDataItem, ISavable
        where T : IAppExtensionItemDefinition
    {
    }
}