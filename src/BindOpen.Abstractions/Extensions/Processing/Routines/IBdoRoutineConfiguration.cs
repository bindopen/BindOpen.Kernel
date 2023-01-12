using BindOpen.Meta.Elements;
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
        new IBdoRoutineConfiguration Add(params IBdoMetaElement[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoRoutineConfiguration WithItems(params IBdoMetaElement[] items);
    }
}