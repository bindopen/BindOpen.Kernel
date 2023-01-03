using BindOpen.Data.Elements;
using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions.Processing
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
        new IBdoRoutineConfiguration Add(params IBdoElement[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoRoutineConfiguration WithItems(params IBdoElement[] items);
    }
}