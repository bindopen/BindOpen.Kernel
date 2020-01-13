using BindOpen.Framework.Data.Items;
using BindOpen.Framework.Extensions.Definition;

namespace BindOpen.Framework.Extensions.Runtime
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