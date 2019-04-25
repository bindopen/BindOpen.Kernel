using BindOpen.Framework.Core.Data.Elements.Source;
using BindOpen.Framework.Core.Extensions.Items.Connectors;

namespace BindOpen.Framework.Core.Data.Elements.Factories
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
            params IConnectorConfiguration[] items)
        {
            return CreateSource(name, null, null, null, items);
        }

        /// <summary>
        /// Initializes a new source element.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static SourceElement CreateSource(
            params IConnectorConfiguration[] items)
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
            params IConnectorConfiguration[] items)
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
            params IConnectorConfiguration[] items)
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
