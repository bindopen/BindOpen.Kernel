using System;

namespace BindOpen.Framework.Core.Extensions.Definition.Routines
{
    public interface IRoutineDefinition : ITAppExtensionItemDefinition<IRoutineDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}