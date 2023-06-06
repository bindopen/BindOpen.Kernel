using BindOpen.Data.Meta;

namespace BindOpen.Extensions.Tasks
{
    /// <summary>
    /// This interface defines a configuration.
    /// </summary>
    public interface IBdoTaskConfiguration : IBdoConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="items"></param>
        /// <returns></returns>
        new IBdoTaskConfiguration Add(params IBdoMetaData[] metas);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="items"></param>
        /// <returns></returns>
        new IBdoTaskConfiguration With(params IBdoMetaData[] metas);
    }
}