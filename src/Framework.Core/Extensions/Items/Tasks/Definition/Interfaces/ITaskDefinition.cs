using System;
using BindOpen.Framework.Core.Extensions.Items.Tasks.Definition.Dto;

namespace BindOpen.Framework.Core.Extensions.Items.Tasks.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITaskDefinition : ITAppExtensionItemDefinition<ITaskDefinitionDto>
    {
        /// <summary>
        /// 
        /// </summary>
        Type RuntimeType { get; set; }
    }
}