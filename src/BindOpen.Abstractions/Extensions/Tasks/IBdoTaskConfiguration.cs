namespace BindOpen.Data.Meta
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
        new IBdoTaskConfiguration Add(
            params IBdoMetaData[] metas);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="items"></param>
        /// <returns></returns>
        new IBdoTaskConfiguration With(
            params IBdoMetaData[] metas);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="items"></param>
        /// <returns></returns>
        IBdoTaskConfiguration AddInputs(
            params IBdoMetaData[] inputs);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="items"></param>
        /// <returns></returns>
        IBdoTaskConfiguration WithInputs(
            params IBdoMetaData[] inputs);

        IBdoTaskConfiguration AddOutputs(
            params IBdoMetaData[] outputs);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="items"></param>
        /// <returns></returns>
        IBdoTaskConfiguration WithOutputs(
            params IBdoMetaData[] outputs);
    }
}