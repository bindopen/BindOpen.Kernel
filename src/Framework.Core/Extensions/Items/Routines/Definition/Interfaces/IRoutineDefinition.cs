using System;
using BindOpen.Framework.Core.Extensions.Items.Routines.Definition.Dto;

namespace BindOpen.Framework.Core.Extensions.Items.Routines.Definition
{
    public interface IRoutineDefinition : ITAppExtensionItemDefinition<IRoutineDefinitionDto>
    {
        Type RuntimeType { get; set; }
    }
}