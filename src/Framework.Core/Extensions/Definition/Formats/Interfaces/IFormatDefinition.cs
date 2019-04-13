using System;

namespace BindOpen.Framework.Core.Extensions.Definition.Formats
{
    public interface IFormatDefinition : ITAppExtensionItemDefinition<IFormatDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}