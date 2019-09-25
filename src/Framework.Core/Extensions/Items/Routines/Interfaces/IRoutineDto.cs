using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.Extensions.Items.Routines.Definition;
using BindOpen.Framework.Core.System.Diagnostics.Events;

namespace BindOpen.Framework.Core.Extensions.Items.Routines
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRoutineConfiguration : ITAppExtensionTitledItemConfiguration<IRoutineDefinition>
    {
        /// <summary>
        /// 
        /// </summary>

        DataItemSet<ConditionalEvent> OutputEventSet { get; set; }
    }
}