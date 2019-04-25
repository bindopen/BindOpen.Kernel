using BindOpen.Framework.Core.Extensions.Items.Handlers;

namespace BindOpen.Framework.Core.Extensions.Definitions.Handlers
{
    public interface IHandlerDefinition : ITAppExtensionItemDefinition<IHandlerDefinitionDto>
    {
        HandlerGetFunction RuntimeFunctionGet { get; set; }
        HandlerPostFunction RuntimeFunctionPost { get; set; }
    }
}