using System;

namespace BindOpen.Framework.Extensions.Definition
{
    /// <summary>
    /// This interface defines the carrier definition.
    /// </summary>
    public interface IBdoCarrierDefinition : ITBdoExtensionItemDefinition<IBdoCarrierDefinitionDto>
    {
        /// <summary>
        /// 
        /// </summary>
        Type RuntimeType { get; set; }
    }
}