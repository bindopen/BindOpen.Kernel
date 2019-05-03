using System;
using BindOpen.Framework.Core.Extensions.Items.Formats.Definition.Dto;

namespace BindOpen.Framework.Core.Extensions.Items.Formats.Definition
{
    public interface IFormatDefinition : ITAppExtensionItemDefinition<IFormatDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}