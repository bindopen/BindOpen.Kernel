using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Definition;

namespace BindOpen.Framework.Core.Extensions.Items
{
    /// <summary>
    /// This class represents an application extension runtime item.
    /// </summary>
    public interface ITAppExtensionItem<T> : IAppExtensionItem, IIdentifiedDataItem
        where T : IAppExtensionItemDefinition
    {
        ITAppExtensionItemDto<T> Dto { get; }

        T Definition { get; }

        void SetDefinition(T definition);
    }
}

