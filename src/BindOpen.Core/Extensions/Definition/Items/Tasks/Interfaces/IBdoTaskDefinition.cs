using System;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoTaskDefinition : ITBdoExtensionItemDefinition<IBdoTaskDefinitionDto>
    {
        /// <summary>
        /// 
        /// </summary>
        Type RuntimeType { get; set; }
    }
}