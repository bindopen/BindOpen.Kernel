using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dto;

namespace BindOpen.Framework.Core.Extensions.Items
{
    /// <summary>
    /// This class represents an application extension runtime item.
    /// </summary>
    public interface ITAppExtensionItem<T> : IAppExtensionItem, IIdentifiedDataItem, INamed
        where T : IAppExtensionItemDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        new ITAppExtensionItemConfiguration<T> Configuration { get; }

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

