using BindOpen.Framework.Core.Extensions.Items.Handlers.Definition.Dto;

namespace BindOpen.Framework.Core.Extensions.Items.Handlers.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHandlerDefinition : ITAppExtensionItemDefinition<IHandlerDefinitionDto>
    {
        /// <summary>
        /// 
        /// </summary>
        HandlerGetFunction RuntimeFunctionGet { get; set; }

        /// <summary>
        /// 
        /// </summary>
        HandlerPostFunction RuntimeFunctionPost { get; set; }
    }
}