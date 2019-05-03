using System;
using BindOpen.Framework.Core.Extensions.Items.Tasks.Definition.Dto;

namespace BindOpen.Framework.Core.Extensions.Items.Tasks.Definition
{
    public interface ITaskDefinition : ITAppExtensionItemDefinition<ITaskDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}