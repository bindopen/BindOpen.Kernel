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
        DataSchema Schema { get; set; }
    }
}