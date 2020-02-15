using BindOpen.Data.Items;
using BindOpen.Extensions.Definition;
using BindOpen.System.Diagnostics.Events;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoRoutineConfiguration : ITBdoExtensionTitledItemConfiguration<IBdoRoutineDefinition>
    {
        /// <summary>
        /// 
        /// </summary>

        DataItemSet<BdoConditionalEvent> OutputEventSet { get; set; }
    }
}