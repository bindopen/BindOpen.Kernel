using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Schema;
using BindOpen.Framework.Core.Extensions.Definitions.Formats;

namespace BindOpen.Framework.Core.Extensions.Definitions.Entities
{
    public interface IEntityDefinitionDto : IAppExtensionItemDefinitionDto
    {
        List<FormatDefinitionDto> FormatDefinitions { get; }
        string ItemClass { get; set; }
        EntityKind Kind { get; set; }
        List<DataSchema> PossibleMetaSchemas { get; }
        string ViewerClass { get; set; }
        DataElementSpecSet DetailSpec { get; set; }

        IFormatDefinitionDto GetFormatWithId(string id);
        IFormatDefinitionDto GetFormatWithName(string name);
        IFormatDefinitionDto GetFormatWithUniqueName(string uniqueName);
    }
}