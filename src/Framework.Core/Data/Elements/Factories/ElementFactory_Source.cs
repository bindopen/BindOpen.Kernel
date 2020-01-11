using BindOpen.Framework.Data.Elements;
using BindOpen.Framework.Extensions.Runtime;

namespace BindOpen.Framework.Data.Elements
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
        /// <param name="items">The items to consider.</param>
        public static SourceElement CreateSource(
            string name,
            params IBdoConnectorConfiguration[] items)
        {
            return CreateSource(name, null, null, null, items);
        }

        /// <summary>
        /// Initializes a new source element.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static SourceElement CreateSource(
            params IBdoConnectorConfiguration[] items)
        {
            return CreateSource(null, null, null, null, items);
        }

        /// <summary>
        /// Initializes a new source element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionUniqueId ">The definition unique ID to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static SourceElement CreateSource(
            string name,
            string definitionUniqueId,
            params IBdoConnectorConfiguration[] items)
        {
            return CreateSource(name, null, definitionUniqueId, null, items);
        }

        /// <summary>
        /// Initializes a new source element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="definitionUniqueId ">The definition unique ID to consider.</param>
        /// <param name="specification">The specification to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static SourceElement CreateSource(
            string name,
            string id,
            string definitionUniqueId,
            ISourceElementSpec specification,
            params IBdoConnectorConfiguration[] items)
        {
            SourceElement element = new SourceElement(name, id)
            {
                DefinitionUniqueId = definitionUniqueId,
                Specification = specification as SourceElementSpec
            };
            element.SetItem(items);

            return element;
        }
    }
}
