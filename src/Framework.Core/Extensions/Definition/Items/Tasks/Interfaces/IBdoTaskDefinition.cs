using System;

namespace BindOpen.Framework.Core.Extensions.Definition.Items
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