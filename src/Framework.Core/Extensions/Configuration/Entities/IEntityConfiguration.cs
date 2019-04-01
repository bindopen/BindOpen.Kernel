using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Schema;
using BindOpen.Framework.Core.Extensions.Definition.Entities;

namespace BindOpen.Framework.Core.Extensions.Configuration.Entities
{
    public interface IEntityConfiguration : ITAppExtensionTitledItemConfiguration<IEntityDefinition>
    {
        IDataElementSet Detail { get; set; }
        IDataSchema Schema { get; set; }
    }
}