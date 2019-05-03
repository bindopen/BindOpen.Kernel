using BindOpen.Framework.Core.Extensions.Items.Handlers.Definition.Dto;

namespace BindOpen.Framework.Core.Extensions.Items.Handlers.Definition
{
    public interface IHandlerDefinition : ITAppExtensionItemDefinition<IHandlerDefinitionDto>
    {
        HandlerGetFunction RuntimeFunctionGet { get; set; }
        HandlerPostFunction RuntimeFunctionPost { get; set; }
    }
}