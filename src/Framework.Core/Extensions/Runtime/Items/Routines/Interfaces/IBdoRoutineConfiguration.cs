using BindOpen.Framework.Data.Items;
using BindOpen.Framework.Extensions.Definition;
using BindOpen.Framework.System.Diagnostics.Events;

namespace BindOpen.Framework.Extensions.Runtime
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