using System;

namespace BindOpen.Framework.Core.Extensions.Definition.Carriers
{
    public interface ICarrierDefinition : ITAppExtensionItemDefinition<ICarrierDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}