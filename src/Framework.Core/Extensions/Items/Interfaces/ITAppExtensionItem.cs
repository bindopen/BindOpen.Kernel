using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dto;
using BindOpen.Framework.Core.Extensions.Definitions;

namespace BindOpen.Framework.Core.Extensions.Items
{
    /// <summary>
    /// This class represents an application extension runtime item.
    /// </summary>
    public interface ITAppExtensionItem<T> : IAppExtensionItem, IIdentifiedDataItem, INamed
        where T : IAppExtensionItemDefinition
    {
        new ITAppExtensionItemConfiguration<T> Configuration { get; }

        T Definition { get; }

        void SetDefinition(T definition);
    }
}

