using System;

namespace BindOpen.Framework.Core.Extensions.Definitions.Connectors
{
    public interface IConnectorDefinition : ITAppExtensionItemDefinition<IConnectorDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}