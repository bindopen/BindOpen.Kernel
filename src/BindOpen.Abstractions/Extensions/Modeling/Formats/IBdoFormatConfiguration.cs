using BindOpen.Runtime.Definition;
using BindOpen.Data.Meta;
using BindOpen.Extensions;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    ///
    /// </summary>
    public interface IBdoFormatConfiguration : ITBdoExtensionItemConfiguration<IBdoFormatDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoFormatConfiguration Add(params IBdoMetaData[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoFormatConfiguration WithItems(params IBdoMetaData[] items);

    }
}