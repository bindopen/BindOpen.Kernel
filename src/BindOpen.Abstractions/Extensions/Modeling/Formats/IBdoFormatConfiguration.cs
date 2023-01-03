using BindOpen.Runtime.Definition;
using BindOpen.Data.Elements;
using BindOpen.Extensions;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    ///
    /// </summary>
    public interface IBdoFormatConfiguration : ITBdoExtensionTitledItemConfiguration<IBdoFormatDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoFormatConfiguration Add(params IBdoElement[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoFormatConfiguration WithItems(params IBdoElement[] items);

    }
}