using System;
using BindOpen.Framework.Core.Extensions.Items.Entities.Definition.Dto;

namespace BindOpen.Framework.Core.Extensions.Items.Entities.Definition
{
    public interface IEntityDefinition : ITAppExtensionItemDefinition<IEntityDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}