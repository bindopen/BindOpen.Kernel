using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items.Schema;
using BindOpen.Framework.Core.Extensions.Definition.Formats;

namespace BindOpen.Framework.Core.Extensions.Definition.Entities
{
    public interface IEntityDefinition : IAppExtensionItemDefinition
    {
        List<IFormatDefinition> FormatDefinitions { get; }
        string ItemClass { get; set; }
        EntityKind Kind { get; set; }
        List<IDataSchema> PossibleMetaSchemas { get; }
        string ViewerClass { get; set; }

        IFormatDefinition GetFormatWithId(string id);
        IFormatDefinition GetFormatWithName(string name);
        IFormatDefinition GetFormatWithUniqueName(string uniqueName);
    }
}