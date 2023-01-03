using BindOpen.Data.Elements;
using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoEntityConfiguration : ITBdoExtensionTitledItemConfiguration<IBdoEntityDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoEntityConfiguration Add(params IBdoElement[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoEntityConfiguration WithItems(params IBdoElement[] items);

        /// <summary>
        /// 
        /// </summary>
        IDataSchema Schema { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        IBdoEntityConfiguration WithSchema(IDataSchema schema);
    }
}