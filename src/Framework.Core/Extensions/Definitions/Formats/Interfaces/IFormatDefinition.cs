using System;

namespace BindOpen.Framework.Core.Extensions.Definitions.Formats
{
    public interface IFormatDefinition : ITAppExtensionItemDefinition<IFormatDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}