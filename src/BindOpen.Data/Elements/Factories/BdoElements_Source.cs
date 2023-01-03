using BindOpen.Extensions.Connecting;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This static class provides methods to create data els.
    /// </summary>
    public static partial class BdoElements
    {
        /// <summary>
        /// Initializes a new source el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="definitionUniqueId ">The definition unique ID to consider.</param>
        public static SourceElement NewSource(
            string name,
            string id = null,
            string definitionUniqueId = null)
            => NewSource<SourceElement>(name, id, definitionUniqueId);

        /// <summary>
        /// Initializes a new source el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="item">The items to consider.</param>
        public static SourceElement NewSource(
            string name,
            IBdoConnectorConfiguration item)
            => NewSource<SourceElement>(name, item);

        /// <summary>
        /// Initializes a new source el.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="item">The items to consider.</param>
        public static SourceElement NewSource(
            string name,
            string id,
            IBdoConnectorConfiguration item)
            => NewSource<SourceElement>(name, id, item);

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
            where T : class, ISourceElement, new()
        {
            var el = new SourceElement(name, id)
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
            where T : class, ISourceElement, new()
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
            where T : class, ISourceElement, new()
        {
            var el = NewSource<T>(name, id, item?.DefinitionUniqueId);
            el.WithItem(item);

            return el;
        }
    }
}
