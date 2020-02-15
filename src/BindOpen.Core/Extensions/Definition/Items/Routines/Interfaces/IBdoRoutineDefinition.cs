using System;

namespace BindOpen.Extensions.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoRoutineDefinition : ITBdoExtensionItemDefinition<IBdoRoutineDefinitionDto>
    {
        /// <summary>
        /// 
        /// </summary>
        Type RuntimeType { get; set; }
    }
}