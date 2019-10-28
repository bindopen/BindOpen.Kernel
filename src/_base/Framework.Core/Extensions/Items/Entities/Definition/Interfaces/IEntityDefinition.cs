using System;
using BindOpen.Framework.Core.Extensions.Items.Entities.Definition.Dto;

namespace BindOpen.Framework.Core.Extensions.Items.Entities.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEntityDefinition : ITAppExtensionItemDefinition<IEntityDefinitionDto>
    {
        /// <summary>
        /// 
        /// </summary>
        Type RuntimeType { get; set; }
    }
}