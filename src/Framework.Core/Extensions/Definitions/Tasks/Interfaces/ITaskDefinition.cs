using System;

namespace BindOpen.Framework.Core.Extensions.Definitions.Tasks
{
    public interface ITaskDefinition : ITAppExtensionItemDefinition<ITaskDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}