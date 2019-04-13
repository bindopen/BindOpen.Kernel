using BindOpen.Framework.Core.Data.Items.Schema;
using BindOpen.Framework.Core.Extensions.Definition.Entities;

namespace BindOpen.Framework.Core.Extensions.Items.Entities
{
    public interface IEntityDto : ITAppExtensionTitledItemDto<EntityDefinition>
    {
        DataSchema Schema { get; set; }
    }
}