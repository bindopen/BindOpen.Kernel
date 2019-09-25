using System;
using BindOpen.Framework.Core.Extensions.Items.Formats.Definition.Dto;

namespace BindOpen.Framework.Core.Extensions.Items.Formats.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFormatDefinition : ITAppExtensionItemDefinition<IFormatDefinitionDto>
    {
        /// <summary>
        /// 
        /// </summary>
        Type RuntimeType { get; set; }
    }
}