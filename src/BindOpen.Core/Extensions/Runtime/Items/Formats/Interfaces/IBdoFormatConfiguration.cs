using BindOpen.Data.Elements;
using BindOpen.Extensions.Definition;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    ///
    /// </summary>
    public interface IBdoFormatConfiguration : ITBdoExtensionTitledItemConfiguration<BdoFormatDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoFormatConfiguration Add(params IDataElement[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoFormatConfiguration WithItems(params IDataElement[] items);

    }
}