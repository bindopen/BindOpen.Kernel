using System;

namespace BindOpen.Framework.Core.Extensions.Definitions.Routines
{
    public interface IRoutineDefinition : ITAppExtensionItemDefinition<IRoutineDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}