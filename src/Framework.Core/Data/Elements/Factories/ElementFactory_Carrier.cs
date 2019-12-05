using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Extensions.Runtime.Items;

namespace BindOpen.Framework.Core.Data.Elements
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
        /// <param name="items">The items to consider.</param>
        public static CarrierElement CreateCarrier(
            string name,
            params IBdoCarrierConfiguration[] items)
        {
            return CreateCarrier(name, null, null, null, items);
        }

        /// <summary>
        /// Initializes a new carrier element.
        /// </summary>
        /// <param name="items">The items to consider.</param>
        public static CarrierElement CreateCarrier(
            params IBdoCarrierConfiguration[] items)
        {
            return CreateCarrier(null, null, null, null, items);
        }

        /// <summary>
        /// Initializes a new carrier element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionUniqueId ">The definition unique ID to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static CarrierElement CreateCarrier(
            string name,
            string definitionUniqueId,
            params IBdoCarrierConfiguration[] items)
        {
            return CreateCarrier(name, null, definitionUniqueId, null, items);
        }

        /// <summary>
        /// Initializes a new carrier element.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="id">The ID to consider.</param>
        /// <param name="definitionUniqueId ">The definition unique ID to consider.</param>
        /// <param name="specification">The specification to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static CarrierElement CreateCarrier(
            string name,
            string id,
            string definitionUniqueId,
            ICarrierElementSpec specification,
            params IBdoCarrierConfiguration[] items)
        {
            CarrierElement element = new CarrierElement(name, id)
            {
                DefinitionUniqueId = definitionUniqueId,
                Specification = specification as CarrierElementSpec
            };
            element.SetItem(items);

            return element;
        }
    }
}
