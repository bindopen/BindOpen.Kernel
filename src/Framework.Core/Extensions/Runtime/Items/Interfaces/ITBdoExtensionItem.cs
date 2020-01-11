using BindOpen.Framework.Data.Items;
using BindOpen.Framework.Extensions.Definition;

namespace BindOpen.Framework.Extensions.Runtime
{
    /// <summary>
    /// This class represents a BindOpen extension runtime item.
    /// </summary>
    public interface ITBdoExtensionItem<T> : IBdoExtensionItem, IIdentifiedDataItem, INamed
        where T : IBdoExtensionItemDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        new ITBdoExtensionItemConfiguration<T> Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        T Definition { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="definition"></param>
        void SetDefinition(T definition);
    }
}

