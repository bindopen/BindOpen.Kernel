using BindOpen.Data.Elements;
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
        /// <param name="items"></param>
        new IBdoRoutineConfiguration Add(params IDataElement[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoRoutineConfiguration WithItems(params IDataElement[] items);

        /// <summary>
        /// 
        /// </summary>

        DataItemSet<BdoConditionalEvent> OutputEventSet { get; set; }
    }
}