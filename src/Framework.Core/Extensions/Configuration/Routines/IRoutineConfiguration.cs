using BindOpen.Framework.Core.Application.Commands;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.Extensions.Definition.Routines;
using BindOpen.Framework.Core.System.Diagnostics.Events;

namespace BindOpen.Framework.Core.Extensions.Configuration.Routines
{
    public interface IRoutineConfiguration : ITAppExtensionTitledItemConfiguration<IRoutineDefinition>
    {
        IDataItemSet<ICommand> CommandSet { get; set; }
        IDataItemSet<IConditionalEvent> OutputEventSet { get; set; }
        IDataElementSet ParameterDetail { get; set; }

        IDataItemSet<ICommand> NewCommandSet();
        IDataItemSet<IConditionalEvent> NewOutputEventSet();
        IDataElementSet NewParameterDetail();
    }
}