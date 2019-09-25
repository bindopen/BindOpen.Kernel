using System;
using BindOpen.Framework.Core.Extensions.Items.Routines.Definition.Dto;

namespace BindOpen.Framework.Core.Extensions.Items.Routines.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRoutineDefinition : ITAppExtensionItemDefinition<IRoutineDefinitionDto>
    {
        /// <summary>
        /// 
        /// </summary>
        Type RuntimeType { get; set; }
    }
}