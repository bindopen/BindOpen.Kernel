using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using BindOpen.Extensions.Definition;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoEntityConfiguration : ITBdoExtensionTitledItemConfiguration<BdoEntityDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoEntityConfiguration Add(params IDataElement[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoEntityConfiguration WithItems(params IDataElement[] items);

        /// <summary>
        /// 
        /// </summary>
        DataSchema Schema { get; set; }
    }
}