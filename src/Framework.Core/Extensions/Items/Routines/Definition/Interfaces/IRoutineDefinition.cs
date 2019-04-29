using System;

namespace BindOpen.Framework.Core.Extensions.Items.Routines.Definition
{
    public interface IRoutineDefinition : ITAppExtensionItemDefinition<IRoutineDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}