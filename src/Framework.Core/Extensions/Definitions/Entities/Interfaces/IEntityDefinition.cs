using System;

namespace BindOpen.Framework.Core.Extensions.Definitions.Entities
{
    public interface IEntityDefinition : ITAppExtensionItemDefinition<IEntityDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}