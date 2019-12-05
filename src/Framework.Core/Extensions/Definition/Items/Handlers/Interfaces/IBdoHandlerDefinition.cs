using BindOpen.Framework.Core.Extensions.Runtime.Items;

namespace BindOpen.Framework.Core.Extensions.Definition.Items
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