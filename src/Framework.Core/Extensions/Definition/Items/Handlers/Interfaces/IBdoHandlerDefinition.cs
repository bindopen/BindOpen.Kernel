using BindOpen.Framework.Extensions.Runtime;

namespace BindOpen.Framework.Extensions.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoHandlerDefinition : ITBdoExtensionItemDefinition<IBdoHandlerDefinitionDto>
    {
        /// <summary>
        /// 
        /// </summary>
        BdoHandlerGetFunction RuntimeFunctionGet { get; set; }

        /// <summary>
        /// 
        /// </summary>
        BdoHandlerPostFunction RuntimeFunctionPost { get; set; }
    }
}