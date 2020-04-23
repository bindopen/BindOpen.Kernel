using BindOpen.Extensions.Runtime;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This static class provides methods to create data elements.
    /// </summary>
    public static partial class ElementFactory
    {
        /// <summary>
        /// Initializes a new source element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="definitionUniqueId ">The definition unique ID to consider.</param>
        public static SourceElement CreateSource(
            string name,
            string id = null,
            string definitionUniqueId = null)
        {
            var element = new SourceElement(name, id)
            {
                DefinitionUniqueId = definitionUniqueId,
            };
            return element;
        }

        /// <summary>
        /// Initializes a new source element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="item">The items to consider.</param>
        public static SourceElement CreateSource(
            string name,
            IBdoConnectorConfiguration item)
        {
            return CreateSource(name, null, item);
        }

        /// <summary>
        /// Initializes a new source element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="item">The items to consider.</param>
        public static SourceElement CreateSource(
            string name,
            string id,
            IBdoConnectorConfiguration item)
        {
            var element = CreateSource(name, id, item?.DefinitionUniqueId);
            element.WithItems(item);

            return element;
        }
    }
}
