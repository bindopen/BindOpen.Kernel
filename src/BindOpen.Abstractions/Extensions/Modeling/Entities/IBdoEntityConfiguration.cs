using BindOpen.Data.Meta;
using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    /// This interface defines the entity configuration.
    /// </summary>
    public interface IBdoEntityConfiguration : ITBdoExtensionItemConfiguration<IBdoEntityDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoEntityConfiguration Add(params IBdoMetaData[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoEntityConfiguration WithItems(params IBdoMetaData[] items);
    }
}