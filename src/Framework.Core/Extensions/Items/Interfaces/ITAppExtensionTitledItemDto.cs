using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dto;
using BindOpen.Framework.Core.Extensions.Definition;

namespace BindOpen.Framework.Core.Extensions.Items
{
    public interface ITAppExtensionTitledItemDto<T>
        : ITAppExtensionItemDto<T>, IGloballyTitled, INamed, IIdentifiedDataItem, ISavable
        where T : IAppExtensionItemDefinition
    {
    }
}