using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.Extensions.Definition.Routines;
using BindOpen.Framework.Core.System.Diagnostics.Events;

namespace BindOpen.Framework.Core.Extensions.Items.Routines
{
    public interface IRoutineDto : ITAppExtensionTitledItemDto<IRoutineDefinition>
    {
        //DataItemSet<Command> CommandSet { get; set; }

        DataItemSet<ConditionalEvent> OutputEventSet { get; set; }
    }
}