using System;

namespace BindOpen.Framework.Core.Extensions.Definition.Entities
{
    public interface IEntityDefinition : ITAppExtensionItemDefinition<IEntityDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}