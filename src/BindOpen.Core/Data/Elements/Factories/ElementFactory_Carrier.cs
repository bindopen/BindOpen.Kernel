using BindOpen.Extensions.Runtime;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// This static class provides methods to create data elements.
    /// </summary>
    public static partial class ElementFactory
    {
        /// <summary>
        /// Initializes a new carrier element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="definitionUniqueId ">The definition unique ID to consider.</param>
        public static CarrierElement CreateCarrier(
            string name,
            string id = null,
            string definitionUniqueId = null)
        {
            var element = new CarrierElement(name, id)
            {
                DefinitionUniqueId = definitionUniqueId,
            };

            return element;
        }

        /// <summary>
        /// Initializes a new carrier element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="item">The items to consider.</param>
        public static CarrierElement CreateCarrier(
            string name,
            IBdoCarrierConfiguration item)
        {
            return CreateCarrier(name, null, item);
        }

        /// <summary>
        /// Initializes a new carrier element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="item">The items to consider.</param>
        public static CarrierElement CreateCarrier(
            string name,
            string id,
            IBdoCarrierConfiguration item)
        {
            var element = CreateCarrier(name, id, item?.DefinitionUniqueId);
            element.WithItems(item);

            return element;
        }
    }
}
