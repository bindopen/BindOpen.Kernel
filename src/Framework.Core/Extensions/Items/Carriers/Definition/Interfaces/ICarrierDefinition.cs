using System;
using BindOpen.Framework.Core.Extensions.Items;

namespace BindOpen.Framework.Core.Extensions.Items.Carriers.Definition
{
    public interface ICarrierDefinition : ITAppExtensionItemDefinition<ICarrierDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}