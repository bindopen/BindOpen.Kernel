using BindOpen.Framework.Core.Data.Items.Schema;
using BindOpen.Framework.Core.Extensions.Definition.Items;

namespace BindOpen.Framework.Core.Extensions.Runtime.Items
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