using System;
using BindOpen.Framework.Core.Extensions.Items.Connectors.Definition.Dto;

namespace BindOpen.Framework.Core.Extensions.Items.Connectors.Definition
{
    public interface IConnectorDefinition : ITAppExtensionItemDefinition<IConnectorDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}