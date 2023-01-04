using BindOpen.Data.Items;
using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This class represents a BindOpen extension runtime item.
    /// </summary>
    public interface ITBdoExtensionItem<D, C, T> : IBdoExtensionItem, ITIdentifiedPoco<T>,
        ITBdoExtensionDefinable<D>, ITBdoExtensionConfigurable<D, C>
        where D : IBdoExtensionItemDefinition
        where C : ITBdoExtensionItemConfiguration<D>
        where T : IBdoExtensionItem
    {
        /// <summary>
        /// Sets the specified definition of this instance.
        /// </summary>
        /// <param name="def">The definition to consider.</param>
        T WithDefinition(D def);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        T WithConfiguration(C config);
    }
}

