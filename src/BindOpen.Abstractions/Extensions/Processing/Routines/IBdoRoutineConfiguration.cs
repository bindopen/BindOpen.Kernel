using BindOpen.Data.Meta;
using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions.Processing
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoRoutineConfiguration : ITBdoExtensionItemConfiguration<IBdoRoutineDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoRoutineConfiguration Add(params IBdoMetaData[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoRoutineConfiguration WithItems(params IBdoMetaData[] items);
    }
}