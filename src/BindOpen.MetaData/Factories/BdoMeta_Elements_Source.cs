using BindOpen.Extensions.Connecting;
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
        public static BdoMetaSource NewSource(
            string name = null,
            string definitionUniqueId = null,
            params IBdoConnectorConfiguration[] items)
            => NewSource<BdoMetaSource>(name, definitionUniqueId, items);

        /// <summary>
        /// Initializes a new carrier el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="item">The items to consider.</param>
        public static BdoMetaSource NewSource(
            string name,
            params IBdoConnectorConfiguration[] items)
            => NewSource<BdoMetaSource>(name, items);

        // Static T creators -------------------------

        /// <summary>
        /// Initializes a new carrier el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionUniqueId ">The definition unique ID to consider.</param>
        public static T NewSource<T>(
            string name = null,
            string definitionUniqueId = null,
            params IBdoConnectorConfiguration[] items)
            where T : class, IBdoMetaSource, new()
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
        public static T NewSource<T>(
            string name,
            params IBdoConnectorConfiguration[] items)
            where T : class, IBdoMetaSource, new()
            => NewSource<T>(name, null as string, items);
    }
}
