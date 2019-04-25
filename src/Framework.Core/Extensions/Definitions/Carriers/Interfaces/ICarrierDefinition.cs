using System;

namespace BindOpen.Framework.Core.Extensions.Definitions.Carriers
{
    public interface ICarrierDefinition : ITAppExtensionItemDefinition<ICarrierDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}