using BindOpen.Data;
using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This class represents a BindOpen extension runtime item.
    /// </summary>
    public interface ITBdoExtensionItem<T, D> :
        IBdoExtensionItem, IIdentified,
        ITBdoExtensionDefinable<D>, IBdoConfigurable
        where T : IBdoExtensionItem
        where D : IBdoExtensionItemDefinition
    {
    }
}

