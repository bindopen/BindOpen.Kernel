using System;

namespace BindOpen.Framework.Core.Extensions.Definition.Connectors
{
    public interface IConnectorDefinition : ITAppExtensionItemDefinition<IConnectorDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}