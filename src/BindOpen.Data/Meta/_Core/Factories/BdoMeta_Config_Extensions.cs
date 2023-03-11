namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This static class provides methods to handle configs.
    /// </summary>
    public partial class BdoConfig
    {
        // NewExtension

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param definitionUniqueName="items">The items to consider.</param>
        public static BdoConfiguration NewExtension(
            params IBdoMetaData[] items)
        => NewExtension<BdoConfiguration>(items);

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param definitionUniqueName="definitionUniqueName">The definitionUniqueName to consider.</param>
        /// <param definitionUniqueName="items">The items to consider.</param>
        public static BdoConfiguration NewExtension(
            string definitionUniqueName,
            params IBdoMetaData[] items)
        => NewExtension<BdoConfiguration>(definitionUniqueName, items);

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param definitionUniqueName="definitionUniqueName">The definitionUniqueName to consider.</param>
        /// <param definitionUniqueName="items">The items to consider.</param>
        public static BdoConfiguration NewExtension(
            string definitionUniqueName,
            string[] usingIds,
            params IBdoMetaData[] items)
        => NewExtension<BdoConfiguration>(definitionUniqueName, usingIds, items);

        // NewExtension<T>

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param definitionUniqueName="definitionUniqueName">The definitionUniqueName to consider.</param>
        /// <param definitionUniqueName="items">The items to consider.</param>
        public static T NewExtension<T>(
            string definitionUniqueName,
            params IBdoMetaData[] items)
            where T : BdoConfiguration, new()
        => NewExtension<T>(definitionUniqueName, null as string[], items);

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param definitionUniqueName="items">The items to consider.</param>
        public static T NewExtension<T>(
            params IBdoMetaData[] items)
            where T : BdoConfiguration, new()
        => NewExtension<T>(null as string, null as string[], items);

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param definitionUniqueName="definitionUniqueName">The definitionUniqueName to consider.</param>
        /// <param definitionUniqueName="items">The items to consider.</param>
        public static T NewExtension<T>(
            string definitionUniqueName,
            string[] usingIds,
            params IBdoMetaData[] items)
            where T : BdoConfiguration, new()
        {
            var config = New<T>(definitionUniqueName, items);
            config.WithDefinitionUniqueName(definitionUniqueName);
            config.Using(usingIds);
            return config;
        }
    }
}
