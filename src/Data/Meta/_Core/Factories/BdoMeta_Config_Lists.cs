namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This static class provides methods to handle configs.
    /// </summary>
    public static partial class BdoMeta
    {
        /// <summary>
        /// Instantiates a new instance of the BdoConfigurationSet class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        public static BdoConfigurationSet NewConfigSet(
            params IBdoConfiguration[] items)
        {
            var config = BdoData.NewSet<BdoConfigurationSet, IBdoConfiguration>(items);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static T NewConfigSet<T>(
            params IBdoConfiguration[] items)
            where T : BdoConfigurationSet, new()
        {
            var list = new T();
            list.With(items);
            return list;
        }
    }
}
