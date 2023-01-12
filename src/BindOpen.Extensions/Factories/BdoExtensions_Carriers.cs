using BindOpen.Extensions.Modeling;
using BindOpen.Meta;
using BindOpen.Meta.Elements;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This static class provides methods to create extension items.
    /// </summary>
    public static partial class BdoExtensions
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
        public static BdoCarrierConfiguration NewCarrierConfiguration(
            string definitionUniqueId,
            params IBdoMetaElement[] items)
        {
            var config = new BdoCarrierConfiguration(definitionUniqueId);
            config.WithItems(items);

            return config;
        }
    }
}
