using System;

namespace BindOpen.Framework.Core.Extensions.Items.Formats.Definition
{
    public interface IFormatDefinition : ITAppExtensionItemDefinition<IFormatDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}