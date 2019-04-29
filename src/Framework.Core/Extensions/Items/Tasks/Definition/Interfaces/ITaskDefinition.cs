using System;

namespace BindOpen.Framework.Core.Extensions.Items.Tasks.Definition
{
    public interface ITaskDefinition : ITAppExtensionItemDefinition<ITaskDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}