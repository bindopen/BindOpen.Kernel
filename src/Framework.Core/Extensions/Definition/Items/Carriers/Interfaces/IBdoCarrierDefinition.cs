using System;

namespace BindOpen.Framework.Core.Extensions.Definition.Items
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