using System;

namespace BindOpen.Framework.Core.Extensions.Definition.Tasks
{
    public interface ITaskDefinition : ITAppExtensionItemDefinition<ITaskDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}