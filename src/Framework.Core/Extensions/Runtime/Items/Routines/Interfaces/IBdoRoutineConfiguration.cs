using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.Extensions.Definition.Items;
using BindOpen.Framework.Core.System.Diagnostics.Events;

namespace BindOpen.Framework.Core.Extensions.Runtime.Items
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