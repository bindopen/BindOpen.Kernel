using System;

namespace BindOpen.Framework.Core.Extensions.Items.Entities.Definition
{
    public interface IEntityDefinition : ITAppExtensionItemDefinition<IEntityDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}