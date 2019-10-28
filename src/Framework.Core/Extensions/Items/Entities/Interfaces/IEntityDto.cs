using BindOpen.Framework.Core.Data.Items.Schema;
using BindOpen.Framework.Core.Extensions.Items.Entities.Definition;

namespace BindOpen.Framework.Core.Extensions.Items.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEntityConfiguration : ITAppExtensionTitledItemConfiguration<EntityDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        DataSchema Schema { get; set; }
    }
}