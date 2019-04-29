using System;

namespace BindOpen.Framework.Core.Extensions.Items.Connectors.Definition
{
    public interface IConnectorDefinition : ITAppExtensionItemDefinition<IConnectorDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}