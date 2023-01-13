using BindOpen.Extensions.Modeling;
using BindOpen.MetaData;
using BindOpen.MetaData.Elements;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This static class provides methods to create extension items.
    /// </summary>
    public static partial class BdoExt
    {
        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <typeparam name="T">The connector class to return.</typeparam>
        /// <param name="config"></param>
        /// <returns>Returns the created connector.</returns>
        public static T NewCarrier<T>(IBdoCarrierConfiguration config)
            where T : class, IBdoCarrier, new()
        {
            T carrier = new();
            carrier.WithConfiguration(config);
            carrier.UpdateFromElementSet<BdoMetaAttribute>(config);

            return carrier;
        }

        /// <summary>
        /// Creates the instance of the specified configuration.
        /// </summary>
        /// <param name="definitionUniqueId">The unique ID of the definition to consider.</param>
        public static BdoCarrierConfiguration NewCarrierConfig(
            string definitionUniqueId,
            params IBdoMetaElement[] items)
        {
            var config = new BdoCarrierConfiguration(definitionUniqueId);
            config.WithItems(items);

            return config;
        }
    }
}
