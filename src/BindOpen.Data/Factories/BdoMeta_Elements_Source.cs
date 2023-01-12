using BindOpen.Extensions.Connecting;
using BindOpen.Meta.Elements;

namespace BindOpen.Meta
{
    /// <summary>
    /// This static class provides methods to create data els.
    /// </summary>
    public static partial class BdoMeta
    {
        /// <summary>
        /// Initializes a new source el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="definitionUniqueId ">The definition unique ID to consider.</param>
        public static BdoMetaSource NewSource(
            string name,
            string id = null,
            string definitionUniqueId = null)
            => NewSource<BdoMetaSource>(name, id, definitionUniqueId);

        /// <summary>
        /// Initializes a new source el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="item">The items to consider.</param>
        public static BdoMetaSource NewSource(
            string name,
            IBdoConnectorConfiguration item)
            => NewSource<BdoMetaSource>(name, item);

        /// <summary>
        /// Initializes a new source el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="item">The items to consider.</param>
        public static BdoMetaSource NewSource(
            string name,
            string id,
            IBdoConnectorConfiguration item)
            => NewSource<BdoMetaSource>(name, id, item);

        // Static T creators -------------------------

        /// <summary>
        /// Initializes a new source el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="definitionUniqueId ">The definition unique ID to consider.</param>
        public static T NewSource<T>(
            string name,
            string id = null,
            string definitionUniqueId = null)
            where T : class, IBdoMetaSource, new()
        {
            var el = new BdoMetaSource(name, id)
            {
                DefinitionUniqueId = definitionUniqueId,
            };
            return el as T;
        }

        /// <summary>
        /// Initializes a new source el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="item">The items to consider.</param>
        public static T NewSource<T>(
            string name,
            IBdoConnectorConfiguration item)
            where T : class, IBdoMetaSource, new()
            => NewSource<T>(name, null, item);

        /// <summary>
        /// Initializes a new source el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="item">The items to consider.</param>
        public static T NewSource<T>(
            string name,
            string id,
            IBdoConnectorConfiguration item)
            where T : class, IBdoMetaSource, new()
        {
            var el = NewSource<T>(name, id, item?.DefinitionUniqueId);
            el.WithItem(item);

            return el;
        }
    }
}
