using BindOpen.Data.Configuration;
using BindOpen.Data.Meta;
using BindOpen.Extensions;

namespace BindOpen.Data
{
    /// <summary>
    /// This static class provides methods to handle configs.
    /// </summary>
    public partial class BdoConfig
    {
        // New

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        public static BdoConfiguration New(
            params IBdoMetaData[] items)
        {
            var config = New<BdoConfiguration>(items);
            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
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
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static BdoConfiguration New(
            string name,
            string[] usingIds,
            params IBdoMetaData[] items)
        {
            var config = New<BdoConfiguration>(name, usingIds, items);
            return config;
        }

        // New<T>

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static T New<T>(
            string name,
            params IBdoMetaData[] items)
            where T : BdoConfiguration, new()
            => New<T>(name, null as string[], items);

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        public static T New<T>(
            params IBdoMetaData[] items)
            where T : BdoConfiguration, new()
            => New<T>(null as string, null as string[], items);

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static T New<T>(
            string name,
            string[] usingIds,
            params IBdoMetaData[] items)
            where T : BdoConfiguration, new()
        {
            var config = BdoMeta.NewSet<T>(items)
                .WithName(name)
                .Using(usingIds);
            return config;
        }

        // NewFrom<T>

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static IBdoConfiguration NewFrom<T>(
            string name,
            T obj)
            where T : IBdoExtension, new()
        {
            var config = new BdoConfiguration()
                .WithDefinitionUniqueName(name);
            //.UpdateFrom(obj);

            return config;
        }

        /// <summary>
        /// Instantiates a new instance of the BdoBaseConfiguration class.
        /// </summary>
        /// <param key="items">The items to consider.</param>
        public static IBdoConfiguration NewFrom<T>(T obj)
            where T : IBdoExtension, new()
            => NewFrom<T>(null, obj);
    }
}
