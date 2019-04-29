using BindOpen.Framework.Core.Data.Items.Schema;
using BindOpen.Framework.Core.Extensions.Items.Entities.Definition;

namespace BindOpen.Framework.Core.Extensions.Items.Entities
{
    public interface IEntityConfiguration : ITAppExtensionTitledItemConfiguration<EntityDefinition>
    {
        DataSchema Schema { get; set; }
    }
}