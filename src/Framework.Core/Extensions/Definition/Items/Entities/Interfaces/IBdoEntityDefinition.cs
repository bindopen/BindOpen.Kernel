using System;

namespace BindOpen.Framework.Extensions.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoEntityDefinition : ITBdoExtensionItemDefinition<IBdoEntityDefinitionDto>
    {
        /// <summary>
        /// 
        /// </summary>
        Type RuntimeType { get; set; }
    }
}