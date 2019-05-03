using System;
using BindOpen.Framework.Core.Extensions.Items.Carriers.Definition.Dto;

namespace BindOpen.Framework.Core.Extensions.Items.Carriers.Definition
{
    public interface ICarrierDefinition : ITAppExtensionItemDefinition<ICarrierDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}