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
        /// Instantiates a new instance of the BdoConfigurationBundle class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static BdoConfigurationBundle NewBundle(
            params IBdoConfiguration[] items)
        {
            var config = BdoData.NewItemSet<BdoConfigurationBundle, IBdoConfiguration>(items);
            return config;
        }

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
        public static BdoConfiguration NewFrom(object obj)
        {
            var config = New<BdoConfiguration>(obj.ToMetaArray());

            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static T New<T>(
            string name,
            params IBdoMetaData[] items)
            where T : BdoConfiguration, new()
        {
            var config = BdoData.NewItemSet<T, IBdoMetaData>(items);
            config.WithName(name);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static T New<T>(
            params IBdoMetaData[] items)
            where T : BdoConfiguration, new()
        {
            var config = BdoData.NewItemSet<T, IBdoMetaData>(items);
            return config;
        }

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
            var config = New<T>(name, items);
            config.Using(usingIds);
            return config;
        }
    }
}
