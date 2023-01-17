using BindOpen.Extensions.Modeling;
using BindOpen.MetaData.Elements;

namespace BindOpen.MetaData
{
    /// <summary>
    /// This static class provides methods to create data els.
    /// </summary>
    public static partial class BdoMeta
    {
        /// <summary>
        /// Initializes a new carrier el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionUniqueId ">The definition unique ID to consider.</param>
        public static BdoMetaCarrier NewCarrier(
            string name = null,
            string definitionUniqueId = null,
            params IBdoCarrierConfiguration[] items)
            => NewCarrier<BdoMetaCarrier>(name, definitionUniqueId, items);

        /// <summary>
        /// Initializes a new carrier el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="item">The items to consider.</param>
        public static BdoMetaCarrier NewCarrier(
            string name,
            params IBdoCarrierConfiguration[] items)
            => NewCarrier<BdoMetaCarrier>(name, items);

        // Static T creators -------------------------

        /// <summary>
        /// Initializes a new carrier el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionUniqueId ">The definition unique ID to consider.</param>
        public static T NewCarrier<T>(
            string name = null,
            string definitionUniqueId = null,
            params IBdoCarrierConfiguration[] items)
            where T : class, IBdoMetaCarrier, new()
        {
            var el = new T();
            el.WithName(name);
            el.WithDefinitionUniqueId(definitionUniqueId);
            el.WithItems(items);

            return el;
        }

        /// <summary>
        /// Initializes a new carrier el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="item">The items to consider.</param>
        public static T NewCarrier<T>(
            string name,
            params IBdoCarrierConfiguration[] items)
            where T : class, IBdoMetaCarrier, new()
            => NewCarrier<T>(name, null as string, items);
    }
}
