using BindOpen.Data.Configuration;
using BindOpen.Data.Meta;

namespace BindOpen.Data
{
    /// <summary>
    /// This static class provides methods to handle configs.
    /// </summary>
    public partial class BdoConfig
    {
        /// <summary>
        /// Instantiates a new instance of the BdoConfigurationSet class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static BdoConfigurationSet NewSet(
            params IBdoConfiguration[] items)
        {
            var config = BdoData.NewList<BdoConfigurationSet, IBdoConfiguration>(items);
            return config;
        }

        // New

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static BdoConfiguration New(
            params IBdoMetaData[] items)
        {
            var config = New<BdoConfiguration>(items);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoConfiguration New(
            string name,
            params IBdoMetaData[] items)
        {
            var config = New<BdoConfiguration>(name, items);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static BdoConfiguration New(
            string name,
            string[] usingIds,
            params IBdoMetaData[] items)
        {
            var config = New<BdoConfiguration>(name, usingIds, items);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoConfiguration class.
        /// </summary>
        /// <param name="obj">The object to consider.</param>
        public static BdoConfiguration NewFrom(
            object obj)
        => NewFrom<BdoConfiguration>(obj);

        // New<T>

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static T New<T>(
            string name,
            params IBdoMetaData[] items)
            where T : BdoConfiguration, new()
        => New<T>(name, null as string[], items);

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static T New<T>(
            params IBdoMetaData[] items)
            where T : BdoConfiguration, new()
        => New<T>(null as string, null as string[], items);

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static T New<T>(
            string name,
            string[] usingIds,
            params IBdoMetaData[] items)
            where T : BdoConfiguration, new()
        {
            var config = BdoMeta.NewList<T>(items);
            config.WithName(name);
            config.Using(usingIds);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoConfiguration class.
        /// </summary>
        /// <param name="obj">The object to consider.</param>
        public static T NewFrom<T>(
            object obj)
            where T : BdoConfiguration, new()
        {
            var config = New<T>(obj.ToMetaArray());
            return config;
        }
    }
}
